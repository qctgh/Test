using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.IService;
using PersonalWebsite.ZaoJiao.Models;
using System.Diagnostics;

namespace PersonalWebsite.ZaoJiao.Controllers
{
    public class HomeController : Controller
    {
        protected ISongService SongService;
        protected ISongMenuService SongMenuService;
        public HomeController(ISongService SongService, ISongMenuService SongMenuService)
        {
            this.SongService = SongService;
            this.SongMenuService = SongMenuService;
        }

        public IActionResult Index()
        {
            HomeIndexModel model = new HomeIndexModel();
            //获取随机推荐歌单
            var songMenus = SongMenuService.GetRecommend(9);
            var tangPoetry = SongService.GetTangPoetry();
            model.SongMenus = songMenus;
            model.Song = tangPoetry;
            return View(model);
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Msg(string content)
        {
            ViewBag.Content = content;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
