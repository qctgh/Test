using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersonalWebsite.IService;
using PersonalWebsite.Todo369.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonalWebsite.Todo369.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<ArticleController> _logger;
        IConfiguration Configuration;
        IUserService UserService;
        public UserController(ILogger<ArticleController> logger, IConfiguration Configuration, IUserService UserService)
        {
            _logger = logger;
            this.Configuration = Configuration;
            this.UserService = UserService;
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
                            return Redirect(returnUrl);
                        else
                            return RedirectToAction("index", "home");
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
                                return Redirect(returnUrl);
                            else
                                return RedirectToAction("index", "home");
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
                return Redirect(returnUrl);
            else
                return RedirectToAction("index", "home");
        }

        public IActionResult CallBack()
        {
            return View();
        }
    }
}