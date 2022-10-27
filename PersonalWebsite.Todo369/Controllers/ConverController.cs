using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Helper;

namespace PersonalWebsite.Todo369.Controllers
{
    public class ConverController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string content)
        {
            ViewBag.Content = content;
            string result = "";
            try
            {
                result = NumberHelper.ConvertToChineseNumber(double.Parse(content));
            }
            catch
            {
                result = "请输入正确的阿拉伯数字";
            }
            ViewBag.Result = result;
            return View();
        }

    }
}