using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class FilterWordController : Controller
    {
        IFilterWordService FilterWordService { get; set; }
        public FilterWordController(IFilterWordService FilterWordService)
        {
            this.FilterWordService = FilterWordService;
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List(int page, int limit)
        {
            var articles = FilterWordService.GetAll(limit, (page - 1) * limit);
            Result result = new Result();
            result.Code = 0;
            result.Data = articles;
            result.Count = FilterWordService.GetAll().Length;
            return Json(result);
        }
    }
}