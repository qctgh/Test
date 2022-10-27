using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class KeyValueController : Controller
    {
        IKeyValueService KeyValueService { get; set; }
        public KeyValueController(IKeyValueService KeyValueService)
        {
            this.KeyValueService = KeyValueService;
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List(string str, int page, int limit)
        {
            var keyValues = KeyValueService.GetAll(limit, (page - 1) * limit);
            Result result = new Result();
            result.Code = 0;
            result.Data = keyValues;
            result.Count = (int)KeyValueService.Count();
            return Json(result);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(KeyValueAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new FormResult() { status = 1, msg = "数据验证未通过" });
            }
            KeyValueService.AddNew(model.Key, model.Value);
            return Json(new FormResult() { status = 0, msg = "保存成功" });
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var keyValue = KeyValueService.GetById(id);
            KeyValueEditModel model = new KeyValueEditModel
            {
                Id = keyValue.Id,
                Key = keyValue.Key,
                Value = keyValue.Value
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(KeyValueEditModel model)
        {
            FormResult result = new FormResult();
            try
            {
                KeyValueService.Edit(model.Id, model.Key, model.Value);
                result.msg = "保存成功";
            }
            catch
            {
                result.msg = "保存失败";
            }
            return Json(result);
        }
    }
}