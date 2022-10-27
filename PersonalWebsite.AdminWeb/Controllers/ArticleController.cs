using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.AdminWeb.Service;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Result = PersonalWebsite.AdminWeb.Models.Result;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly ElasticClient _client;
        private readonly IHostingEnvironment _hostingEnvironment;
        IArticleService ArticleService { get; set; }
        IChannelService ChannelService { get; set; }
        IAdminUserService AdminUserService { get; set; }
        ISettingService SettingService { get; set; }
        IFileService FileService { get; set; }

        public ArticleController(ILogger<ArticleController> logger, IEsClientProvider clientProvider, IHostingEnvironment hostingEnvironment, IArticleService ArticleService, IChannelService ChannelService, IAdminUserService AdminUserService, ISettingService SettingService, IFileService FileService)
        {
            _logger = logger;
            _client = clientProvider.GetClient();
            _hostingEnvironment = hostingEnvironment;
            this.ArticleService = ArticleService;
            this.ChannelService = ChannelService;
            this.AdminUserService = AdminUserService;
            this.SettingService = SettingService;
            this.FileService = FileService;
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
        public IActionResult List(string title, int page, int limit)
        {
            ArticleDTO[] articles;
            int count;

            Result result = new Result();
            result.Code = 0;

            if (!string.IsNullOrEmpty(title))
            {

                articles = ArticleService.GetAll(title, limit, (page - 1) * limit);
                count = (int)ArticleService.Count(title);
            }
            else
            {
                articles = ArticleService.GetAll(limit, (page - 1) * limit);
                count = (int)ArticleService.Count();
            }
            result.Data = articles;
            result.Count = count;
            return Json(result);
        }
        [HttpGet]
        public IActionResult Add()
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
                    var childChannel = cmList.FirstOrDefault(p => p.Id == item.ParentId);
                    childChannel.Channels.Add(new ChannelModel { Id = item.Id, Code = item.Code, Name = item.Name, ParentId = item.ParentId });
                }

            }
            ViewBag.Channels = cmList;

            return View();
        }
        [HttpPost]
        public IActionResult Add(ArticleModel model)
        {
            string userId = HttpContext.Session.GetString("UserId");

            var article = ArticleService.AddArticle(model.Title, model.Abstract, model.Classification, model.Cover, model.ChannelId, model.Content, model.SupportCount, model.IsFirst, long.Parse(userId));
            //保存到缓存
            //IndexResponse response = _client.IndexDocument(article);
            _logger.LogInformation($"添加文章成功，标题：{model.Title}");
            return Json(new FormResult { status = 0, msg = "保存成功" });
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var article = ArticleService.GetById(id);
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
                    var childChannel = cmList.FirstOrDefault(p => p.Id == item.ParentId);
                    childChannel.Channels.Add(new ChannelModel { Id = item.Id, Code = item.Code, Name = item.Name, ParentId = item.ParentId });
                }

            }
            ViewBag.Channels = cmList;

            ArticleModel model = new ArticleModel
            {
                Id = article.Id,
                Title = article.Title,
                Abstract = article.Abstract,
                Classification = article.Classification,
                Cover = article.Cover,
                SupportCount = int.Parse(article.SupportCount),
                IsFirst = article.IsFirst,
                ChannelId = article.ChannelId,
                Content = article.Content,
                UserId = article.UserId,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ArticleModel model)
        {
            FormResult result = new FormResult();
            //编辑文章
            bool isSuccess = ArticleService.Edit(model.Id, model.Title, model.Abstract, model.Classification, model.Cover, model.ChannelId, model.Content, model.SupportCount, model.IsFirst, 1);
            if (isSuccess)
            {
                result.status = 0;
                result.msg = "保存成功";
            }
            else
            {
                result.status = 1;
                result.msg = "保存失败";
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
            _logger.LogWarning($"删除文章，文章ID：{id}");
            return Json(new Result { Code = 0, Msg = "删除成功" });
        }
        //todo:搜索
        public IActionResult Search(string key)
        {
            var articleDTOs = _client.Search<ArticleDTO>(s => s
                 .From(0)
                 .Size(10)
                 .Query(q => q.QueryString(qs => qs.Query(key).DefaultOperator(Operator.And)))).Documents.ToArray();
            //.Query(q => q.Match(m => m.Field(f => f.Content).Query(key)))).Documents;
            Result result = new Result();
            result.Code = 0;
            result.Data = articleDTOs;
            result.Count = articleDTOs.Length;
            return Json(result);
        }


        ////七牛云上传图片
        //[HttpPost]
        //public IActionResult Upload()
        //{
        //    long size = 0;
        //    var path = "";
        //    var files = Request.Form.Files;
        //    var file = files[0];
        //    //foreach (var file in files)
        //    //{
        //    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //    string fileExt = Path.GetExtension(file.FileName); //文件扩展名
        //    long fileSize = file.Length; //获得文件大小，以字节为单位
        //    string newFileName = Guid.NewGuid().ToString() + fileExt; //随机生成新的文件名
        //    path = DateTime.Now.ToString("yyyy/MM/dd/") + newFileName;
        //    //path = "upload/" + DateTime.Now.ToString("yyyy/MM/dd/") + newFileName;
        //    //上传至本地
        //    //path = _hostingEnvironment.WebRootPath + $@"\{path}";
        //    //new FileInfo(path).Directory.Create();//尝试创建可能不存在的文件夹
        //    //size += file.Length;
        //    //using (FileStream fs = System.IO.File.Create(path))
        //    //{
        //    //    file.CopyTo(fs);
        //    //    fs.Flush();
        //    //}

        //    //上传至云存储
        //    bool isUploadStatus = UploadImg(file.OpenReadStream(), path);

        //    path = "http://q9nee8182.bkt.clouddn.com/" + path;
        //    //}
        //    return Json(new UploadResult { Status = isUploadStatus ? 0 : 1, Msg = "上传成功", Url = path }); ;
        //}

        /// <summary>
        /// 上传图片至七牛云对象存储
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


        #region huangdi
        // 录入页面数据通过规则
        public IActionResult AddHuangDi()
        {
            Result result = new Result();
            try
            {
                //从配置文件获取文章规则Url
                string artRuleUrl = "http://www.51xingjy.com/book/zhongyililun/lingshu/";
                string hrefRegex = "<li><a(.+?)href=\"(.+?)\"(.*?)>(.+?)</a></li>";
                string contentRegex = "<div id=\"htmlContent\" class=\"contentbox\">(.+?)</div>";

                var data = GetHtmlStr(artRuleUrl, "GB2312");
                string temp = data.Substring(3300).Replace("\r", " ").Replace("\n", " ");
                var matches = Regex.Matches(temp, hrefRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                ArticleDTO article = new ArticleDTO();
                if (matches.Count < 250)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        Match match = matches[i];
                        string href = match.Groups[2].Value;
                        var content = GetHtmlStr(href, "gb2312");
                        var contentMatches = Regex.Match(content.Replace("\r", " ").Replace("\n", " "), contentRegex);
                        var contentTemp = contentMatches.Value.Remove(0, 109).Replace("</strong>", "").Replace("<font color=\"#ff0000\">", "").Replace("<font color=\"#0000ff\">", "").Replace("<font face=\"黑体\">", "").Replace("<font size=\"5\">", "").Replace("<font >", "").Replace("<strong>", "").Replace("</strong>", "").Replace("</font>", "").Replace("<br /> 　　", "\n").Replace("<br />", "\n");
                        contentTemp = contentTemp.Remove(contentTemp.Length - 105, 105);
                        string title = match.Groups[4].Value;
                        string userId = HttpContext.Session.GetString("UserId");

                        ArticleService.AddArticle(title, null, 1, null, 48, contentTemp, 0, false, long.Parse(userId));
                    }
                }
                result.Code = 0;
                result.Msg = "录入成功";
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Msg = ex.Message;
            }
            return Json(result);
        }

        /// <summary>  
        /// 获取网页的HTML码  
        /// </summary>  
        /// <param name="url">链接地址</param>  
        /// <param name="encoding">编码类型</param>  
        /// <returns></returns>  
        public string GetHtmlStr(string url, string encoding)
        {
            string htmlStr = "";
            try
            {
                if (!String.IsNullOrEmpty(url))
                {
                    WebRequest request = WebRequest.Create(url);            //实例化WebRequest对象  
                    WebResponse response = request.GetResponse();           //创建WebResponse对象  
                    Stream datastream = response.GetResponseStream();       //创建流对象  
                    Encoding ec = Encoding.GetEncoding(encoding);
                    //if (encoding == "UTF8")
                    //{
                    //    ec = Encoding.UTF8;
                    //}
                    //else if (encoding == "Default")
                    //{
                    //    ec = Encoding.Default;
                    //}
                    //else if (encoding == "gb2312")
                    //{
                    //    ec = Encoding.ASCII;
                    //}
                    StreamReader reader = new StreamReader(datastream, ec);
                    htmlStr = reader.ReadToEnd();                  //读取网页内容  
                    reader.Close();
                    datastream.Close();
                    response.Close();
                }
            }
            catch { }
            return htmlStr;
        }
        #endregion

        public IActionResult OneIndex()
        {
            var articles = ArticleService.GetAllField();
            foreach (var article in articles)
            {
                DocumentPath<ArticleDTO> deletePath = new DocumentPath<ArticleDTO>(article.Id);
                _client.Delete(deletePath);
                IndexResponse response = _client.IndexDocument(article);
            }
            return Json(new Result { Code = 1, Msg = "一键全部索引成功" });
        }

    }
}