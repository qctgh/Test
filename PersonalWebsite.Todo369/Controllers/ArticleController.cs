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
    public class ArticleController : Controller
    {
        private readonly ILogger<ArticleController> _logger;
        IArticleService ArticleService { get; set; }
        ICommentService CommentService { get; set; }
        IChannelService ChannelService { get; set; }
        IFilterWordService FilterWordService { get; set; }
        IArticleRateService ArticleRateService { get; set; }
        public ArticleController(ILogger<ArticleController> logger, IArticleService ArticleService, ICommentService CommentService, IChannelService ChannelService, IFilterWordService FilterWordService, IArticleRateService ArticleRateService)
        {
            _logger = logger;
            this.ArticleService = ArticleService;
            this.CommentService = CommentService;
            this.ChannelService = ChannelService;
            this.FilterWordService = FilterWordService;
            this.ArticleRateService = ArticleRateService;
        }
        #region 文章
        public IActionResult Index(string id)
        {
            var channel = ChannelService.GetByCode(id);
            ArticleIndexModel model = new ArticleIndexModel();
            model.Channel = channel;
            return View(model);
        }
        public IActionResult ArticleIndex(string id, int page)
        {
            var articles = ArticleService.GetByChannelId(id, 10, (page - 1) * 10);
            //天花板，3.0取3,3.1取4
            var count = Math.Ceiling(ArticleService.GetByChannelId(id).Length / 10.0);
            Result result = new Result
            {
                Code = 0,
                Data = articles,
                //总页数
                Count = Convert.ToInt32(count)
            };
            return Json(result);
        }

        public IActionResult Detail(int id)
        {
            //频道
            var channels = ChannelService.GetAll();

            //todo:这样设计有个问题，子频道必须在父频道后面，不然会报错
            List<ChannelModel> cmList = new List<ChannelModel>();
            //把所有频道整理成树状结构
            foreach (var item in channels)
            {
                //父亲
                if (item.ParentId == 0)
                {
                    ChannelModel channelModel = new ChannelModel();
                    channelModel.Id = item.Id;
                    channelModel.Code = item.Code;
                    channelModel.Name = item.Name;
                    channelModel.ParentId = item.ParentId;
                    cmList.Add(channelModel);
                }
                //儿子
                else
                {
                    //查找cmList中是否已经存在当前频道的父亲
                    var channel = cmList.FirstOrDefault(p => p.Id == item.ParentId);
                    channel.Channels.Add(new ChannelModel { Id = item.Id, Code = item.Code, Name = item.Name, ParentId = item.ParentId });
                }

            }


            //文章
            var article = ArticleService.GetById(id);
            //评论
            var comments = CommentService.GetByArticleId(id);
            //该频道下的所有文章ID
            var articleIds = ArticleService.GetByChannelId(article.ChannelCode).Select(p => (int)p.Id).ToArray();
            //如果是黄帝内经特殊处理，因为要让黄帝内经升序显示，而Index的要求是倒序
            if (article.ChannelCode == "huangdi")
            {
                articleIds = ArticleService.GetByChannelId(article.ChannelCode).OrderBy(p => p.Id).Select(p => (int)p.Id).ToArray();
            }
            ArticleDetailModel model = new ArticleDetailModel();
            model.Article = article;
            model.Comments = comments;
            model.ArticleIds = string.Join(",", articleIds);
            //确定当前文章的Index值
            int temp = 0;
            for (int i = 0; i < articleIds.Length; i++)
            {
                if (articleIds[i] == (int)article.Id)
                {
                    temp = i + 1;
                }
            }
            model.CurrentIndex = temp;
            return View(model);
        }

        public IActionResult Add()
        {
            return RedirectToAction("Temp", "Home");
        }
        //点赞
        public IActionResult Love(int id)
        {
            Result result = new Result();
            //游客IP
            string ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            int count = ArticleRateService.Get24HRateCount(id, ip);
            if (count > 0)
            {
                result.Code = 1;
                result.Msg = "一天后再来点赞吧~";
                return Json(result);
            }
            //游客的点赞行为记录到表中
            ArticleRateService.Add(id, ip);
            //更新文章的SupportCount字段，通过冗余字段来提高效率
            ArticleService.Love(id);

            result.Msg = "您对这篇文章点赞啦~";
            return Json(result);
        }
        #endregion

        #region 评论
        //评论列表
        public IActionResult CommentList(long id, int page)
        {
            var comments = CommentService.GetByArticleId(id, 3, (page - 1) * 3);
            //天花板，3.0取3,3.1取4
            var count = Math.Ceiling(CommentService.GetByArticleId(id).Length / 3.0);
            Result result = new Result
            {
                Code = 0,
                Data = comments,
                Count = Convert.ToInt32(count)
            };
            return Json(result);
        }

        public IActionResult Comment(long id, string content, string vercode)
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
                CommentService.Add(id, content, ip, false);
                result.Status = 1;
                result.Msg = "请耐心等待审核";
                return Json(result);
            }
            else
            {
                content = replaceMsg;
                CommentService.Add(id, content, ip, true);
                result.Action = Url.Content("~/Article/Detail/" + id);
                return Json(result);
            }
        }


        #endregion

    }
}