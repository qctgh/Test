using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class SongController : Controller
    {
        private readonly IConfiguration _configuration;
        ISongService SongService;
        ISongMenuService SongMenuService;
        public SongController(IConfiguration _configuration, ISongService SongService, ISongMenuService SongMenuService)
        {
            this._configuration = _configuration;
            this.SongService = SongService;
            this.SongMenuService = SongMenuService;
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
        public IActionResult List(int page, int limit)
        {
            var songs = SongService.GetAll(limit, (page - 1) * limit);
            Result result = new Result();
            result.Code = 0;
            result.Data = songs;
            result.Count = (int)SongService.Count();
            return Json(result);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var songMenus = SongMenuService.GetAll();
            ViewBag.SongMenu = songMenus.OrderByDescending(p => p.CreateDateTime).Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() });
            return View();
        }

        [HttpPost]
        [DisableRequestSizeLimit]//上传文件是 post 方式，这里加不加都可以
        public IActionResult Add(SongModel model, List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);       //统计所有文件的大小
            var request = Request;
            var filepath = Directory.GetCurrentDirectory() + "\\file";  //存储文件的路径
            //string filesHub = _configuration["FilesHub"].ToString();

            foreach (var item in files)     //上传选定的文件列表
            {
                string name = Path.GetFileNameWithoutExtension(item.FileName).Replace("'", "’");
                string mp3 = $"{model.Src}{item.FileName}".Replace("'", "’");
                SongService.Add(name, model.Artist, model.Album, model.Cover, mp3, model.SongMenuId);


                //if (item.Length > 0)        //文件大小 0 才上传
                //{
                //    var thispath = filepath + "\\" + item.FileName;     //当前上传文件应存放的位置

                //    if (System.IO.File.Exists(thispath) == true)        //如果文件已经存在,跳过此文件的上传
                //    {
                //        ViewBag.log += "\r\n文件已存在：" + thispath.ToString();
                //        continue;
                //    }

                //    //上传文件
                //    using (var stream = new FileStream(thispath, FileMode.Create))      //创建特定名称的文件流
                //    {
                //        try
                //        {
                //            await item.CopyToAsync(stream);     //上传文件
                //        }
                //        catch (Exception ex)        //上传异常处理
                //        {
                //            ViewBag.log += "\r\n" + ex.ToString();
                //        }
                //    }
                //}
            }
            return Json(new FormResult { status = 0, msg = "保存成功" });
        }





    }
}