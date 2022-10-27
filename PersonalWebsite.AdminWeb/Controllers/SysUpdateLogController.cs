using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class SysUpdateLogController : Controller
    {
        ISysUpdateLogService SysUpdateLogService;
        public SysUpdateLogController(ISysUpdateLogService SysUpdateLogService)
        {
            this.SysUpdateLogService = SysUpdateLogService;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(string content)
        {
            SysUpdateLogService.Add(content);
            return Json(new FormResult { status = 0, msg = "保存成功" });
        }
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List(int page = 1, int limit = 10)
        {
            var sysUpdateLogs = SysUpdateLogService.GetAll(limit, (page - 1) * limit);
            Result result = new Result();
            result.Code = 0;
            result.Data = sysUpdateLogs;
            result.Count = (int)SysUpdateLogService.Count();
            return Json(result);
        }
    }
}