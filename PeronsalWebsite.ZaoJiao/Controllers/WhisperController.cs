using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.IService;
using PersonalWebsite.ZaoJiao.Models;

namespace PersonalWebsite.ZaoJiao.Controllers
{
    public class WhisperController : Controller
    {
        IArticleService ArticleService;
        public WhisperController(IArticleService ArticleService)
        {
            this.ArticleService = ArticleService;
        }

        public IActionResult Index()
        {
            var articles = ArticleService.GetAllFieldByChannelId("zaojiao");

            WhisperIndexModel model = new WhisperIndexModel();
            model.Articles = articles;
            return View(model);
        }

        public IActionResult Detail(long id)
        {
            var article = ArticleService.GetById(id);
            return View(article);
        }
    }
}