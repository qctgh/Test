using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Blog.Filters;
using PersonalWebsite.Blog.Models;
using PersonalWebsite.IService;
using System;
using System.Linq;
using System.Security.Claims;

namespace PersonalWebsite.Blog.Controllers
{
    public class MessageController : Controller
    {

        IMessageService MessageService { get; set; }

        public MessageController(IMessageService MessageService)
        {
            this.MessageService = MessageService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [LoginActionFilter]
        public IActionResult Add(long id, string content)
        {
            //获取评论者IP
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();
            //获取用户ID
            long userId = long.Parse(HttpContext.User.FindAll(ClaimTypes.Sid).First().Value);
            MessageService.Add(2, id, content, userId, ip, true);
            return Redirect(Url.Action("Index", "Message"));
        }

        //评论列表
        public IActionResult List(int page)
        {
            var messages = MessageService.GetAll(2, 3, (page - 1) * 3);
            //天花板，3.0取3,3.1取4
            var count = Math.Ceiling(MessageService.GetAll(2) / 3.0);
            Result result = new Result
            {
                Code = 0,
                Data = messages,
                Count = Convert.ToInt32(count)
            };
            return Json(result);
        }
    }
}