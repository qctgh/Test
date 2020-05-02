using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class ArticleController : Controller
    {
        protected IArticleService ArticleService { get; set; }
        private readonly IHostingEnvironment _hostingEnvironment;

        public ArticleController(IArticleService ArticleService, IHostingEnvironment hostingEnvironment)
        {
            this.ArticleService = ArticleService;
            _hostingEnvironment = hostingEnvironment;
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


        //审核文章
        public IActionResult Check(long id)
        {
            bool isSuccess = ArticleService.CheckById(id);
            return Json(new Result { Code = isSuccess ? 0 : 1, Msg = "审核通过" });
        }

        //删除文章
        public IActionResult Del(long id)
        {
            ArticleService.DeleteById(id);
            return Json(new Result { Code = 0, Msg = "删除成功" });
        }



        //上传图片
        [HttpPost]
        public IActionResult Upload()
        {
            long size = 0;
            var path = "";
            var files = Request.Form.Files;
            var file = files[0];
            //foreach (var file in files)
            //{
            var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string fileExt = Path.GetExtension(file.FileName); //文件扩展名
            long fileSize = file.Length; //获得文件大小，以字节为单位
            string newFileName = Guid.NewGuid().ToString() + fileExt; //随机生成新的文件名
            path = DateTime.Now.ToString("yyyy/MM/dd/") + newFileName;
            //path = "upload/" + DateTime.Now.ToString("yyyy/MM/dd/") + newFileName;
            //上传至本地
            //path = _hostingEnvironment.WebRootPath + $@"\{path}";
            //new FileInfo(path).Directory.Create();//尝试创建可能不存在的文件夹
            //size += file.Length;
            //using (FileStream fs = System.IO.File.Create(path))
            //{
            //    file.CopyTo(fs);
            //    fs.Flush();
            //}

            //上传至云存储
            bool isUploadStatus = UploadImg(file.OpenReadStream(), path);

            path = "http://q9nee8182.bkt.clouddn.com/" + path;
            //}
            return Json(new UploadResult { Status = isUploadStatus ? 0 : 1, Msg = "上传成功", Url = path }); ;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="stream">图片的文件流</param>
        /// <param name="saveKey">保存图片的名称</param>
        /// <returns></returns>
        public bool UploadImg(Stream stream, string saveKey)
        {
            string bucket = "qiect";
            //string saveKey = "girl/gf/a.jpg";
            //string localFile = "D:\\1.jpg";
            Mac mac = new Mac("SpdiTrU7uSzq-Y1kLCzOVPaTGiBwqHBZXXOfIs6Y",
                "-_owpZQDPiTsH3iWZvbfFn_pK4a7KKeUVRQ6Yx5s");
            Qiniu.Common.Config.AutoZone("SpdiTrU7uSzq-Y1kLCzOVPaTGiBwqHBZXXOfIs6Y",
                bucket, true);
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = bucket;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            UploadManager um = new UploadManager();
            //HttpResult result = um.UploadFile(localFile, saveKey, token);
            HttpResult result = um.UploadStream(stream, saveKey, token);
            return result.Code == 200;
        }

    }
}