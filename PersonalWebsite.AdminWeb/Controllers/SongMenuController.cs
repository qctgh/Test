using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.Helper.TagHelpers;
using PersonalWebsite.IService;
using System;
using System.Collections.Generic;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class SongMenuController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        ISongMenuService SongMenuService;
        IKeyValueService KeyValueService;
        public SongMenuController(IConfiguration _configuration, IHostingEnvironment _hostingEnvironment, ISongMenuService SongMenuService, IKeyValueService KeyValueService)
        {
            this._configuration = _configuration;
            this._hostingEnvironment = _hostingEnvironment;
            this.SongMenuService = SongMenuService;
            this.KeyValueService = KeyValueService;
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
            var keyValues = SongMenuService.GetAll(limit, (page - 1) * limit);
            Result result = new Result();
            result.Code = 0;
            result.Data = keyValues;
            result.Count = Convert.ToInt32(SongMenuService.Count());
            return Json(result);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var songMenuTags = KeyValueService.GetAll("SongMenuTag");
            List<CheckBoxItem> items = new List<CheckBoxItem>();
            foreach (var item in songMenuTags)
            {
                items.Add(new CheckBoxItem { Id = Guid.NewGuid().ToString(), Value = item.Id.ToString(), Text = item.Value });
            }
            ViewBag.SongMenuTags = items;
            return View();
        }
        [HttpPost]
        public IActionResult Add(SongMenuModel model)
        {
            //复选框从前台传过来只有一个值，不是一个数组，所以在前台checkbox的name里加了个【】，但是request的时候发现name名变成了tags[1]，这里就排除其他的标签（包含name的），剩下的就是选中的checkbox，特殊处理吧
            List<string> ids = new List<string>();
            for (int i = 1; i <= Request.Form.Count - 6; i++)
            {
                ids.Add(Request.Form[$"tags[{i}]"].ToString());
            }
            FormResult result = new FormResult();
            if (!ModelState.IsValid)
            {
                result.status = 1;
                result.msg = "数据验证未通过";
                return Json(result);
            }
            else
            {
                SongMenuService.Add(model.Name, string.Join(',', ids), model.CoverImgSrc, model.Describe, model.OrderIndex);
                result.msg = "保存成功";
                result.action = Url.Content("~/SongMenu/Add");
                return Json(result);
            }
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            SongMenuModel model = new SongMenuModel();
            var songMenu = SongMenuService.GetById(id);

            model.Name = songMenu.Name;
            model.CoverImgSrc = songMenu.CoverImgSrc.Substring(30, songMenu.CoverImgSrc.Length - 30);
            model.Describe = songMenu.Describe;
            model.OrderIndex = songMenu.OrderIndex.ToString();

            var songMenuTags = KeyValueService.GetAll("SongMenuTag");
            List<CheckBoxItem> items = new List<CheckBoxItem>();
            foreach (var item in songMenuTags)
            {
                items.Add(new CheckBoxItem { Id = Guid.NewGuid().ToString(), Value = item.Id.ToString(), Text = item.Value, Checked = songMenu.Tags.Contains(item.Id.ToString()) });
            }
            ViewBag.SongMenuTags = items;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(SongMenuModel model)
        {
            //复选框从前台传过来只有一个值，不是一个数组，所以在前台checkbox的name里加了个【】，但是request的时候发现name名变成了tags[1]，这里就排除其他的标签（包含name的），剩下的就是选中的checkbox，特殊处理吧
            var songMenuTags = KeyValueService.GetAll("SongMenuTag");
            List<string> ids = new List<string>();
            for (int i = 0; i < songMenuTags.Length; i++)
            {
                ids.Add(Request.Form[$"tags[{i}]"].ToString());
            }
            FormResult result = new FormResult();
            if (!ModelState.IsValid)
            {
                result.status = 1;
                result.msg = "数据验证未通过";
                return Json(result);
            }
            else
            {
                SongMenuService.Edit(model.Id, model.Name, string.Join(',', ids), model.CoverImgSrc, model.Describe, model.OrderIndex);
                result.msg = "保存成功";
                result.action = Url.Content($"~/SongMenu/Edit/{model.Id}");
                return Json(result);
            }
        }

    }
}