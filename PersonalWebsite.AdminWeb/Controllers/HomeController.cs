using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using System.Diagnostics;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class HomeController : Controller
    {
        IAdminUserService AdminUserService { get; set; }

        public HomeController(IAdminUserService AdminUserService)
        {
            this.AdminUserService = AdminUserService;
        }
        public IActionResult Index()
        {
            string userId = HttpContext.Session.GetString("UserId");
            //Session中的UserId为空，则代表未登录或者Session过期，转到登录页
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }
            AdminUserDTO adminUser = AdminUserService.GetById(long.Parse(userId));
            HomeIndexModel model = new HomeIndexModel();
            model.AdminUser = adminUser;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
