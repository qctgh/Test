using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.QQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.IService;
using PersonalWebsite.Service;
using PersonalWebsite.ZaoJiao.Models;
using System;
using System.Linq;
using System.Reflection;

namespace PersonalWebsite.ZaoJiao
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
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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

            //注册认证服务
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    //这里填写一些配置信息，默认即可
                })    //添加Cookie认证
                .AddQQ(qqOptions =>
                {
                    qqOptions.AppId = Configuration["Authentication:QQ:AppId"];    //QQ互联申请的AppId
                    qqOptions.AppKey = Configuration["Authentication:QQ:AppKey"];    //QQ互联申请的AppKey
                    qqOptions.CallbackPath = "/home/index";    //QQ互联回调地址
                                                                  //自定义认证声明
                    qqOptions.ClaimActions.MapJsonKey(MyClaimTypes.QQOpenId, "openid");
                    qqOptions.ClaimActions.MapJsonKey(MyClaimTypes.QQName, "nickname");
                    qqOptions.ClaimActions.MapJsonKey(MyClaimTypes.QQFigure, "figureurl_qq_1");
                    qqOptions.ClaimActions.MapJsonKey(MyClaimTypes.QQGender, "gender");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //使用验证中间件
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
