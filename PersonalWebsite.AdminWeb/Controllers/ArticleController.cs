using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class ArticleController : Controller
    {
        protected IArticleService ArticleService { get; set; }

        public ArticleController(IArticleService ArticleService)
        {
            this.ArticleService = ArticleService;
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
        public IActionResult List(ArticleModel model)
        {
            var articles = ArticleService.GetAll();
            Result result = new Result();
            result.Code = 0;
            result.Data = articles;
            result.Count = articles.Length;
            return Json(result);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ArticleModel model)
        {
            ArticleService.AddArticle(model.Title, model.ChannelId, model.Content, model.SupportCount, model.IsFirst, 1);
            return Json(new Result { Code = 1, Msg = "保存成功" });
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var article = ArticleService.GetById(id);
            ArticleModel model = new ArticleModel
            {
                Id = article.Id,
                Title = article.Title,
                SupportCount = article.SupportCount,
                IsFirst = article.IsFirst,
                ChannelId = article.ChannelId,
                Content = article.Content,
                UserId = article.UserId
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ArticleModel model)
        {
            Result result = new Result();
            //编辑文章
            bool isSuccess = ArticleService.Edit(model.Id, model.Title, model.ChannelId, model.Content, model.SupportCount, model.IsFirst, 1);
            if (isSuccess)
            {
                result.Code = 0;
                result.Msg = "保存成功";
            }
            else
            {
                result.Code = 1;
                result.Msg = "保存失败";
            }
            return Json(result);
        }
        //上传图片
        [HttpPost]
        public IActionResult Upload()
        {
            long size = 0;
            var path = "";
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string fileExt = Path.GetExtension(file.FileName); //文件扩展名
                long fileSize = file.Length; //获得文件大小，以字节为单位
                string newFileName = System.Guid.NewGuid().ToString() + fileExt; //随机生成新的文件名
                path = @"upload\" + newFileName;
                path = Directory.GetCurrentDirectory() + $@"\{path}";
                new FileInfo(path).Directory.Create();//尝试创建可能不存在的文件夹
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(path))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                path = "/upload/" + newFileName;
            }
            return Json(new UploadResult { Status = 0, Msg = "上传成功", Url = path }); ;
        }

    }
}