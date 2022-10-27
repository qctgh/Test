using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class SettingController : Controller
    {
        ISettingService SettingService { get; set; }
        public SettingController(ISettingService SettingService)
        {
            this.SettingService = SettingService;
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List(string str)
        {
            var settings = SettingService.GetAll();
            Result result = new Result();
            result.Code = 0;
            result.Data = settings;
            result.Count = settings.Length;
            return Json(result);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(SettingAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new FormResult() { status = 1, msg = "数据验证未通过" });
            }
            SettingService.SetValue(model.Name, model.Value);
            return Json(new FormResult() { status = 0, msg = "保存成功" });
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var setting = SettingService.GetById(id);
            SettingEditModel model = new SettingEditModel
            {
                Id = setting.Id,
                Name = setting.Name,
                Value = setting.Value
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(SettingEditModel model)
        {
            SettingService.Edit(model.Id, model.Name, model.Value);
            return Json(new FormResult() { status = 0, msg = "保存成功" });
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}