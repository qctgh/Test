using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.AdminWeb.MyQuartz;
using PersonalWebsite.AdminWeb.Service;
using PersonalWebsite.IService;
using PersonalWebsite.Service;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Linq;
using System.Reflection;

namespace PersonalWebsite.AdminWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // 此lambda确定给定请求是否需要用户同意非必要cookie。
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(3);
            });
            services.AddDistributedMemoryCache();

            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));

            services.AddSingleton<IEsClientProvider, EsClientProvider>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region 注入 Quartz调度类
            services.AddSingleton<QuartzOperate>();
            services.AddTransient<SendMail>();
            //注册ISchedulerFactory的实例。
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<IJobFactory, IOCJobFactory>();


            #endregion

            #region 初始化PersonalWebsite.Service所有服务
            //初始化PersonalWebsite.Service所有服务
            var serviceAsm = Assembly.Load(new AssemblyName("PersonalWebsite.Service"));
            foreach (Type serviceType in serviceAsm.GetTypes().Where(t => typeof(IServiceSupport).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract))
            {
                var interfaceTypes = serviceType.GetInterfaces();
                foreach (var interfaceType in interfaceTypes)
                {
                    services.AddScoped(interfaceType, serviceType);
                }
            }
            #endregion

            //#region JWT鉴权授权
            //1.Nuget引入程序包：Microsoft.AspNetCore.Authentication.JwtBearer 
            //services.AddAuthentication();//禁用  
            //var ValidAudience = "http://todo369.club:88";/*SettingService.GetValue("audience");*/
            //var ValidIssuer = "http://todo369.club:88";/*SettingService.GetValue("issuer");*/
            //var SecurityKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDI2a2EJ7m872v0afyoSDJT2o1+SitIeJSWtLJU8/Wz2m7gStexajkeD+Lka6DSTy8gt9UwfgVQo6uKjVLG5Ex7PiGOODVqAEghBuS7JzIYU5RvI543nNDAPfnJsas96mSA7L/mD7RTE2drj6hf3oZjJpMPZUQI/B1Qjb5H3K3PNwIDAQAB";/*SettingService.GetValue("SecurityKey");*/
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  //默认授权机制名称；                                      
            //         .AddJwtBearer(options =>
            //         {
            //             options.TokenValidationParameters = new TokenValidationParameters
            //             {
            //                 ValidateIssuer = true,//是否验证Issuer
            //                 ValidateAudience = true,//是否验证Audience
            //                 ValidateLifetime = true,//是否验证失效时间
            //                 ValidateIssuerSigningKey = true,//是否验证SecurityKey
            //                 ClockSkew = TimeSpan.FromHours(1),
            //                 ValidAudience = ValidAudience,//Audience
            //                 ValidIssuer = ValidIssuer,//Issuer，这两项和前面签发jwt的设置一致  表示谁签发的Token
            //                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey))//拿到SecurityKey
            //                 //AudienceValidator = (m, n, z) =>
            //                 //{
            //                 //    return m != null && m.FirstOrDefault().Equals(this.Configuration["audience"]);
            //                 //},//自定义校验规则，可以新登录后将之前的无效 
            //             };
            //         });
            //#endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            ServiceLocator.Instance = app.ApplicationServices;

            //添加身份验证
            //app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //获取前面注入的Quartz调度类
            var quartz = app.ApplicationServices.GetRequiredService<QuartzOperate>();
            appLifetime.ApplicationStarted.Register(() =>
            {
                quartz.Start().Wait();
            });

            appLifetime.ApplicationStopped.Register(() =>
            {
                quartz.Stop();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
