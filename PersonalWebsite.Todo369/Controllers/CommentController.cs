using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalWebsite.Helper;
using PersonalWebsite.IService;
using PersonalWebsite.Todo369.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.Todo369.Controllers
{
    public class CommentController : Controller
    {
        private readonly ILogger<ArticleController> _logger;
        ICommentService CommentService { get; set; }
        IFilterWordService FilterWordService { get; set; }
        IKeyValueService keyValueService { get; set; }
        public CommentController(ILogger<ArticleController> logger, ICommentService CommentService, IFilterWordService FilterWordService, IKeyValueService keyValueService)
        {
            _logger = logger;
            this.FilterWordService = FilterWordService;
            this.CommentService = CommentService;
            this.keyValueService = keyValueService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Data(int page)
        {
            var comments = CommentService.GetByArticleId(0, 10, (page - 1) * 10);
            comments.ToList().ForEach(p => p.IP = GetRandomName());
            int count = Convert.ToInt32(Math.Ceiling(CommentService.GetByArticleId(0).Length / 10.0));
            Result result = new Result
            {
                Code = 0,
                Data = comments,
                Count = count
            };
            return Json(result);
        }

        public IActionResult Add(string content, string vercode)
        {

            SubmitResult result = new SubmitResult();
            string code = TempData["ValidateCode"].ToString();
            if (!vercode.Equals(code))
            {
                result.Status = 2;
                result.Msg = "验证码错误";
                return Json(result);
            }
            if (string.IsNullOrEmpty(content))
            {
                result.Status = 1;
                result.Msg = "请输入内容";
                return Json(result);
            }
            //获取评论者IP
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();
            string replaceMsg;
            //对用户输入的评论进行过滤处理
            FilterResult filterResult = FilterWordService.FilterMsg(content, out replaceMsg);

            if (filterResult == FilterResult.Banned)
            {
                _logger.LogInformation($"{ip}输入包含禁用词内容：{content}");
                //如果含有禁用词，则不向数据库中插入
                result.Status = 1;
                result.Msg = "您的评论内容含有禁用词汇，请注意文明用语";
                return Json(result);
            }
            else if (filterResult == FilterResult.Mod)
            {
                //含有审核词则向数据库中插入，但是IsVisible=false，需要审核设置为true才能显示
                CommentService.Add(0, content, ip, false);
                result.Status = 1;
                result.Msg = "请耐心等待审核";
                return Json(result);
            }
            else
            {
                content = replaceMsg;
                CommentService.Add(0, content, ip, true);
                result.Action = Url.Content("~/Comment/Index/");
                return Json(result);
            }
        }

        private string GetRandomName()
        {
            //生成随机用户名
            var keyValues = keyValueService.GetAll("随机用户名");
            Random random = new Random();
            int next = random.Next(0, keyValues.Length);
            return keyValues[next].Value;
        }
    }
}