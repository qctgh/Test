using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class UserController : Controller
    {
        IUserService UserService { get; set; }
        public UserController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List(string str)
        {
            var users = UserService.GetAll();
            Result result = new Result();
            result.Code = 0;
            result.Data = users;
            result.Count = users.Length;
            result.Msg = "";
            return Json(result);
        }

    }
}