using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.IService;

namespace PersonalWebsite.Web.Controllers
{
    public class ArticleController : Controller
    {

        public IArticleService ArticleService { get; set; }

        public ArticleController(IArticleService articleService)
        {
            this.ArticleService = articleService;
        }

        public IActionResult Index()
        {
            ArticleService.AddArticle("测试标题", "", 0, "内容", 0);
            return View();
        }
    }
}