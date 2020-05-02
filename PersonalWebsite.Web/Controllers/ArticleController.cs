using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.IService;
using PersonalWebsite.Web.Models;

namespace PersonalWebsite.Web.Controllers
{
    public class ArticleController : Controller
    {
        IArticleService ArticleService { get; set; }
        public ArticleController(IArticleService ArticleService)
        {
            this.ArticleService = ArticleService;
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
            return View(model);
        }
    }
}