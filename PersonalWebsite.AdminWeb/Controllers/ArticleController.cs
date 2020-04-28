using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class ArticleController : Controller
    {
        protected IArticleService ArticleService { get; set; }

        public ArticleController(IArticleService ArticleService)
        {
            this.ArticleService = ArticleService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List(ArticleModel model)
        {
            var articles = ArticleService.GetAll();
            Result result = new Result();
            result.Code = 0;
            result.Data = articles;
            result.Count = articles.Length;
            return Json(result);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ArticleModel model)
        {
            ArticleService.AddArticle(model.Title, model.ChannelId, model.Content, model.SupportCount, model.IsFirst, model.UserId);
            return View();
        }
    }
}