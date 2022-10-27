using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.IService;
using PersonalWebsite.Todo369.Models;

namespace PersonalWebsite.Todo369.Controllers
{
    public class SysUpdateLogController : Controller
    {
        ISysUpdateLogService SysUpdateLogService;
        public SysUpdateLogController(ISysUpdateLogService SysUpdateLogService)
        {
            this.SysUpdateLogService = SysUpdateLogService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List(int id = 1)
        {
            var articles = SysUpdateLogService.GetAll(6, (id - 1) * 6);
            //天花板，3.0取3,3.1取4
            var count = Math.Ceiling(SysUpdateLogService.Count() / 6.0);
            Result result = new Result
            {
                Code = 0,
                Data = articles,
                //总页数
                Count = Convert.ToInt32(count)
            };
            return Json(result);
        }

    }
}