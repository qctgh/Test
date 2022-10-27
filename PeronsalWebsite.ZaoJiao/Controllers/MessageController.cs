using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Helper;
using PersonalWebsite.IService;
using PersonalWebsite.ZaoJiao.Filters;
using PersonalWebsite.ZaoJiao.Models;
using System.Linq;
using System.Security.Claims;

namespace PersonalWebsite.ZaoJiao.Controllers
{
    public class MessageController : Controller
    {
        IMessageService MessageService { get; set; }
        public MessageController(IMessageService MessageService)
        {
            this.MessageService = MessageService;
        }
        //评论列表
        public IActionResult Index(int pageIndex = 1)
        {
            MessageIndexModel model = new MessageIndexModel();
            var messages = MessageService.GetAll(3, 6, (pageIndex - 1) * 6);
            int count = (int)MessageService.GetAll(3);
            //分页
            var ps = new PageString();

            /*可选参数*/

            ps.SetIsEnglish = false;// 是否是英文       (默认：false)
            ps.SetIsShowText = false;//是否显示分页文字 (默认：true)
                                     //ps.TextFormat=""                         (默认值：《span class=\"pagetext\"》《strong》总共《/strong》:{0} 条 《strong》当前《/strong》:{1}/{2}《/span》)
                                     //ps.SetPageIndexName  Request["pageIndex"](默认值："pageIndex")
            ps.SetIsAjax = false;//                    (默认值："false")

            /*函数参数*/
            int total = count;
            int pageSize = 6;
            string url = Url.Content("~/Message/Index?");
            var page = ps.ToString(total, pageSize, pageIndex, url);

            model.Count = count;
            model.Messages = messages;
            model.Page = page;
            return View(model);
        }
        [LoginActionFilter]
        public IActionResult Add(long pid, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return Redirect("~/Message/Index#respond");
            }
            //获取评论者IP
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();
            //获取用户ID
            long userId = long.Parse(HttpContext.User.FindAll(ClaimTypes.Sid).First().Value);
            MessageService.Add(3, pid, message, userId, ip, true);
            return Redirect(Url.Action("Index", "Message"));
        }
    }
}