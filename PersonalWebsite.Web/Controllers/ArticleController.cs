using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Web.Models;

namespace PersonalWebsite.Web.Controllers
{
    public class ArticleController : Controller
    {
        //重用
        private static string bannedExprKey = typeof(ArticleController) + "BannedExpr";
        private static string modExprKey = typeof(ArticleController) + "ModExpr";

        IArticleService ArticleService { get; set; }
        ICommentService CommentService { get; set; }
        IFilterWordService FilterWordService { get; set; }
        public ArticleController(IArticleService ArticleService, ICommentService CommentService, IFilterWordService FilterWordService)
        {
            this.ArticleService = ArticleService;
            this.CommentService = CommentService;
            this.FilterWordService = FilterWordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(int page)
        {
            var articles = ArticleService.GetAll(3, (page - 1) * 3);
            //天花板，3.0取3,3.1取4
            var count = Math.Ceiling(ArticleService.GetAll().Length / 3.0);
            Result result = new Result
            {
                Code = 0,
                Data = articles,
                Count = Convert.ToInt32(count)
            };
            return Json(result);
        }


        public IActionResult Detail(long id)
        {
            var article = ArticleService.GetById(id);
            ArticleDetailModel model = new ArticleDetailModel();
            model.Article = article;
            var comments = CommentService.GetByArticleId(id);
            model.Comments = comments;
            return View(model);
        }
        //写评论
        [HttpGet]
        public IActionResult Comment(long id)
        {
            var article = ArticleService.GetById(id);
            ArticleDetailModel model = new ArticleDetailModel();
            model.Article = article;
            return View(model);
        }
        [HttpPost]
        public IActionResult Comment(long id, string content)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(content))
            {
                result.Code = 1;
                result.Msg = "请输入内容";
                return Json(result);
            }
            //获取评论者IP
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();
            string replaceMsg;
            //对用户输入的评论进行过滤处理
            FilterResult filterResult = FilterMsg(content, out replaceMsg);
            if (filterResult == FilterResult.OK)
            {
                content = replaceMsg;
                CommentService.Add(id, content, ip, true);
            }
            else if (filterResult == FilterResult.Mod)
            {
                //含有审核词则向数据库中插入，但是IsVisible=false，需要审核设置为true才能显示
                CommentService.Add(id, content, ip, false);
                result.Code = 1;
                result.Msg = "请耐心等待审核";
                return Json(result);
            }
            else if (filterResult == FilterResult.Banned)
            {
                //如果含有禁用词，则不向数据库中插入
                result.Code = 1;
                result.Msg = "您的评论内容含有禁用词汇，请注意文明用语";
                return Json(result);
            }
            return RedirectToAction("Detail", new { Id = id });
        }


        /// <summary>
        /// 取得禁用词的正则表达式
        /// 由方法去处理缓存的问题
        /// </summary>
        /// <returns>返回禁用词正则表达式</returns>
        private string GetBannedExpr()
        {
            //因为缓存是整个网站唯一的实例，所以要保证key的唯一性
            //我的习惯就是在Cache的名字前加一个类名，几乎不会重复
            //项目组规定，缓存的名字都是类型全名+缓存项的名字
            string bannedExpr = GetCookies(bannedExprKey);
            if (!string.IsNullOrEmpty(bannedExpr))//如果缓存中存在，则直接返回
            {
                return bannedExpr;
            }

            var banned = (from word in FilterWordService.GetBanned()
                          select word.WordPattern).ToArray();
            bannedExpr = string.Join("|", banned);

            bannedExpr = bannedExpr.Replace(@".", @"\.").Replace("{2}", ".{0,2}").Replace(@"\", @"\\");
            //如果不存在则去数据库取，然后放入缓存
            SetCookies(bannedExprKey, bannedExpr);
            return bannedExpr;
        }


        private string GetModExpr()
        {
            string modExpr = GetCookies(modExprKey);
            if (!string.IsNullOrEmpty(modExpr))//如果缓存中存在，则直接返回
            {
                return modExpr;
            }
            var mod = (from word in FilterWordService.GetMod()
                       select word.WordPattern).ToArray();
            modExpr = string.Join("|", mod);

            modExpr = modExpr.Replace(@".", @"\.").Replace("{2}", ".{0,2}").Replace(@"\", @"\\");
            //如果不存在则去数据库取，然后放入缓存
            SetCookies(modExprKey, modExpr);
            return modExpr;
        }

        private IEnumerable<FilterWordDTO> GetReplaceWords()
        {
            string replaceKey = typeof(ArticleController) + "Replace";
            IEnumerable<FilterWordDTO> data = JsonConvert.DeserializeObject<IEnumerable<FilterWordDTO>>(GetCookies(replaceKey));
            if (data != null)//如果缓存中存在，则直接返回
            {
                return data;
            }
            data = FilterWordService.GetReplace();
            //如果不存在则去数据库取，然后放入缓存
            SetCookies(replaceKey, JsonConvert.SerializeObject(data));
            return data;
        }

        public FilterResult FilterMsg(string msg, out string replacemsg)
        {
            string bannedExpr = GetBannedExpr();

            //todo：避免每次进行过滤词判断的时候都进行数据库查询。缓存，把bannedExpr缓存起来。


            string modExpr = GetModExpr();


            //先判断禁用词！有禁用词就不让发，无从谈起审核词
            //Regex.Replace(
            foreach (var word in GetReplaceWords())
            {
                msg = msg.Replace(word.WordPattern, word.ReplaceWord);
            }
            replacemsg = msg;

            if (Regex.IsMatch(replacemsg, bannedExpr))
            {
                return FilterResult.Banned;
            }
            else if (Regex.IsMatch(replacemsg, modExpr))
            {
                return FilterResult.Mod;
            }


            return FilterResult.OK;
        }



        #region 扩展方法
        /// <summary>
        /// 设置本地cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>  
        /// <param name="minutes">过期时长，单位：分钟</param>      
        protected void SetCookies(string key, string value, int minutes = 30)
        {
            HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }
        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="key">键</param>
        protected void DeleteCookies(string key)
        {
            HttpContext.Response.Cookies.Delete(key);
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        protected string GetCookies(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }
        #endregion
    }

    public enum FilterResult
    {
        OK, Mod, Banned
    }
}