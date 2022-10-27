using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class WorkController : Controller
    {
        private readonly IWorkService WorkService;
        public WorkController(IWorkService WorkService)
        {
            this.WorkService = WorkService;
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
            var songs = WorkService.GetAll(limit, (page - 1) * limit);

            Result result = new Result();
            result.Code = 0;
            result.Data = songs;
            result.Count = (int)WorkService.Count();
            return Json(result);
        }
    }
}