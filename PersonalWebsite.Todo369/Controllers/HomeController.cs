using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebsite.IService;
using PersonalWebsite.Todo369.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PersonalWebsite.Todo369.Controllers
{
    public class HomeController : Controller
    {
        IArticleService ArticleService { get; set; }
        IChannelService ChannelService { get; set; }

        public HomeController(IArticleService ArticleService, IChannelService ChannelService)
        {
            this.ArticleService = ArticleService;
            this.ChannelService = ChannelService;
        }

        public IActionResult Index()
        {
            HomeIndexModel model = new HomeIndexModel();
            //置顶
            var topArticles = ArticleService.GetTop();

            model.TopArticles = topArticles;
            model.ChannelYun = JsonConvert.SerializeObject(GetChannel());
            return View(model);
        }

        //文章列表
        public IActionResult ArticleList(int page)
        {
            var articles = ArticleService.GetAll(10, (page - 1) * 10);
            //天花板，3.0取3,3.1取4
            var count = Math.Ceiling(ArticleService.Count() / 10.0);
            Result result = new Result
            {
                Code = 0,
                Data = articles,
                //总页数
                Count = Convert.ToInt32(count)
            };
            return Json(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Temp()
        {
            return View();
        }

        public IActionResult SystemLog()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<ChannelResultModel> GetChannel()
        {
            var channels = ChannelService.GetAll();
            List<ChannelResultModel> channelResults = new List<ChannelResultModel>();
            foreach (var item in channels)
            {
                channelResults.Add(new ChannelResultModel
                {
                    label = item.Name,
                    url = item.ParentId == 0 ? "#" : $"{Request.Scheme}://{Request.Host}/Article/Index/{item.Code}",
                    target = item.ParentId == 0 ? "_top" : "_blank"
                });
            }
            return channelResults;
        }

    }

    public struct ChannelResultModel
    {
        public string label { get; set; }
        public string url { get; set; }
        public string target { get; set; }
    }
}
