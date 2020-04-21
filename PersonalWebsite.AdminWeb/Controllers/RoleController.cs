using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class RoleController : Controller
    {
        protected IRoleService RoleService { get; set; }

        public RoleController(IRoleService RoleService)
        {
            this.RoleService = RoleService;
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
        public IActionResult List(string st)
        {
            var roles = RoleService.GetAll();
            Result result = new Result();
            result.Code = 0;
            result.Data = roles;
            result.Count = roles.Length;
            return Json(result);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(RoleAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new Result() { Code = 1, Msg = "数据验证未通过" });
            }
            long roleId = RoleService.AddNew(model.Name);
            //给角色添加权限
            //permService.AddPermIds(roleId, model.PermissionIds);
            return Json(new Result() { Code = 0, Msg = "保存成功" });
        }
    }
}