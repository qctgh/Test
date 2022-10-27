using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersonalWebsite.IService;
using PersonalWebsite.ZaoJiao.Filters;
using PersonalWebsite.ZaoJiao.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonalWebsite.ZaoJiao.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        IConfiguration Configuration;
        IUserService UserService;
        IEmailService EmailService;
        IMailService MailService;
        ITimingService TimingService;
        public UserController(ILogger<UserController> logger, IConfiguration Configuration, IUserService UserService, IEmailService EmailService, IMailService MailService, ITimingService TimingService)
        {
            _logger = logger;
            this.Configuration = Configuration;
            this.UserService = UserService;
            this.EmailService = EmailService;
            this.MailService = MailService;
            this.TimingService = TimingService;
        }
        [LoginActionFilter]
        public IActionResult Index()
        {
            long userId = long.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            var user = UserService.GetById(userId);
            UserIndexModel model = new UserIndexModel();
            model.Name = user.Name;
            model.Email = user.Email;
            model.EmailStatus = user.EmailStatus == "active" ? "你的邮箱已激活" : "你的邮箱未激活";
            return View(model);
        }
        [LoginActionFilter]
        [HttpGet]
        public IActionResult Setting()
        {
            //从缓存中获取用户ID
            long userId = long.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            var timing = TimingService.GetByUserId(userId);

            UserSettingModel model = new UserSettingModel();
            Dictionary<string, string> items = new Dictionary<string, string>();
            for (int i = 0; i <= 6; i++)
            {
                items.Add(i.ToString(), (ToWeek(i)));
            }

            model.Time = timing.Time;
            model.Weeks = timing.Weeks;
            model.CBWeeks = items;
            return View(model);
        }
        [LoginActionFilter]
        [HttpPost]
        public IActionResult Setting(string time, string weeks)
        {
            time = Request.Form["time"];
            weeks = Request.Form["weeks"];
            long userId = long.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            TimingService.Modify(userId, time, weeks);
            weeks = weeks.Replace('0', '7');
            string strUrl = Url.Action("Msg", "Home", new { content = $"保存成功，早教启蒙将在每周{weeks}的{time}时发送邮件提醒你。" });
            return Redirect(strUrl);
        }
        [LoginActionFilter]
        public IActionResult Info(UserIndexModel model)
        {
            //验证邮箱格式
            Regex regex = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            if (!regex.IsMatch(model.Email))
            {
                return Redirect(Url.Action("Index", "User"));
            }

            long userId = long.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            var user = UserService.ModifyEmail(userId, model.Email);
            //发送邮件验证
            string result = EmailService.SendValidateEmail(userId, model.Email, user.VCode);
            return Redirect(Url.Action("ValidateUser", "User"));
        }
        public IActionResult ValidateUser(string id)
        {
            UserValidateUserModel model = new UserValidateUserModel();
            //若id为空，则不是激活邮箱的请求
            if (string.IsNullOrEmpty(id))
            {
                model.Msg = "已发送邮件到你的邮箱，快去激活吧！";
            }
            else
            {

                model.Msg = UserService.ValidateVCode(id);
                //我这里的逻辑可以写到一起，没必要controller和service里写两坨逻辑
                //string str = Des.DesDecrypt(id);
                //long userId = long.Parse(str.Split('&')[0]);
                //string email = str.Split('&')[1];
                //string vcode = str.Split('&')[2];
                //var user = UserService.GetById(userId);
                //if (user.EmailStatus == "notactive")
                //{
                //    model.Msg = UserService.ValidateVCode(userId, email, vcode);
                //}
                //else
                //{
                //    model.Msg = "邮箱已经激活，快去看看别的吧！";
                //}
            }
            return View(model);
        }


        public IActionResult Login(string provider = "QQ", string returnUrl = null)
        {
            //第三方登录成功后跳转的地址
            var redirectUrl = Url.Action(nameof(ExternalLoginCallbackAsync), new { returnUrl });
            var properties = new AuthenticationProperties()
            {
                RedirectUri = redirectUrl
            };
            return Challenge(properties, provider);
        }
        [Authorize]
        public async Task<IActionResult> ExternalLoginCallbackAsync(string returnUrl = null)
        {
            //QQ认证后会默认登录，如果你想自定义登录，可以先注销第三方登录的身份
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            string openId = "", name = "", figure = "", gender = "";
            //从当前登录用户的身份声明中获取信息（是否有些眼熟，MyClaimTypes就是在Startup里面自定义的那些）
            foreach (var item in HttpContext.User.Claims)
            {
                switch (item.Type)
                {
                    case MyClaimTypes.QQOpenId:
                        openId = item.Value;
                        break;
                    case MyClaimTypes.QQName:
                        name = item.Value;
                        break;
                    case MyClaimTypes.QQFigure:
                        figure = item.Value;
                        break;
                    case MyClaimTypes.QQGender:
                        gender = item.Value;
                        break;
                    default:
                        break;
                }
            }
            try
            {
                //获取到OpenId后进行登录或者注册（以下作为示范，不要盲目复制粘贴）
                if (!string.IsNullOrEmpty(openId))
                {
                    //去数据库查询该QQ是否绑定用户
                    var user = UserService.GetByQQOpenId(openId);
                    if (user != null)
                    {
                        #region 存在则登陆
                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                        identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                        identity.AddClaim(new Claim(MyClaimTypes.Avator, user.Avatar));
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(int.Parse(Configuration["AppSettings:LoginExpires"]))) // 有效时间
                        });
                        user.LastLoginIP = HttpContext.Connection.RemoteIpAddress.ToString();
                        user.LastLoginTime = DateTime.Now;
                        //更新登录信息
                        UserService.Update(user.QQOpenId, user.LastLoginIP, user.LastLoginTime);
                        #endregion
                        if (returnUrl != null)
                            return Redirect($"~{returnUrl}");
                        else
                            return Redirect("~/");
                    }
                    else
                    {
                        //注册
                        var userDto = UserService.Add(openId, name, figure, gender);
                        if (userDto != null)
                        {
                            #region 注册后自动登陆
                            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                            identity.AddClaim(new Claim(ClaimTypes.Sid, userDto.Id.ToString()));
                            identity.AddClaim(new Claim(ClaimTypes.Name, userDto.Name));
                            identity.AddClaim(new Claim(MyClaimTypes.Avator, userDto.Avatar));
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(int.Parse(Configuration["AppSettings:LoginExpires"]))) // 有效时间
                            });
                            userDto.LastLoginIP = HttpContext.Connection.RemoteIpAddress.ToString();
                            userDto.LastLoginTime = DateTime.Now;
                            //更新登录信息
                            UserService.Update(openId, userDto.LastLoginIP, userDto.LastLoginTime);
                            #endregion
                            if (returnUrl != null)
                                return Redirect($"~{returnUrl}");
                            else
                                return Redirect("~/");
                        }
                        else
                            throw new Exception("Add User failed");
                    }
                }
                else
                    throw new Exception("OpenId is null");
            }
            catch (Exception ex)
            {
                _logger.LogError($"登录发生错误{ex.Message}");
                throw new Exception("登录发生错误");
            }
        }

        public async Task<IActionResult> Out(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (returnUrl != null)
                return Redirect($"~{returnUrl}");
            else
                return Redirect("~/");
        }


        private string ToWeek(int i)
        {
            string result = "";
            switch (i)
            {
                case 0: result = "每周日"; break;
                case 1: result = "每周一"; break;
                case 2: result = "每周二"; break;
                case 3: result = "每周三"; break;
                case 4: result = "每周四"; break;
                case 5: result = "每周五"; break;
                case 6: result = "每周六"; break;
                default: result = "未知数"; break;

            }
            return result;
        }

    }
}