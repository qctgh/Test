using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class TimingController : Controller
    {
        private readonly ITimingService TimingService;
        public TimingController(ITimingService TimingService)
        {
            this.TimingService = TimingService;
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
        public IActionResult List(int page, int limit)
        {
            var songs = TimingService.GetAll(limit, (page - 1) * limit);
           
            Result result = new Result();
            result.Code = 0;
            result.Data = songs;
            result.Count = (int)TimingService.Count();
            return Json(result);
        }
    }
}