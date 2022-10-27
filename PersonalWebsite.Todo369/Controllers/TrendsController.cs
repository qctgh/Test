using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.IService;
using PersonalWebsite.Todo369.Models;

namespace PersonalWebsite.Todo369.Controllers
{
    public class TrendsController : Controller
    {
        IArticleService ArticleService { get; set; }
        public TrendsController(IArticleService ArticleService)
        {
            this.ArticleService = ArticleService;
        }

        public IActionResult Index()
        {
            var articles = ArticleService.GetAllFieldByChannelId("suibi");
            TrendsIndexModel model = new TrendsIndexModel();
            model.Articles = articles;
            return View(model);
        }
    }
}