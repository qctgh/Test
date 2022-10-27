using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class MailController : Controller
    {
        IMailService MailService;
        public MailController(IMailService MailService)
        {
            this.MailService = MailService;
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
            var songs = MailService.GetAll(limit, (page - 1) * limit);
            foreach (var item in songs)
            {
                item.Type = ToType(item.Type);
            }
            Result result = new Result();
            result.Code = 0;
            result.Data = songs;
            result.Count = (int)MailService.Count();
            return Json(result);
        }

        private string ToType(string str)
        {
            string result = "";
            switch (str)
            {
                case "1":
                    result = "用户触发";
                    break;
                case "2":
                    result = "定时发送";
                    break;
                default:
                    result = "错误";
                    break;
            }
            return result;
        }
    }
}