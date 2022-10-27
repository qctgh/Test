using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class RoleController : Controller
    {
        protected IRoleService RoleService { get; set; }
        protected IPermissionService PermissionService { get; set; }

        public RoleController(IRoleService RoleService, IPermissionService PermissionService)
        {
            this.RoleService = RoleService;
            this.PermissionService = PermissionService;
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
                return Json(new FormResult() { status = 1, msg = "数据验证未通过" });
            }
            long roleId = RoleService.AddNew(model.Name);
            //给角色添加权限
            //permService.AddPermIds(roleId, model.PermissionIds);
            return Json(new FormResult() { status = 0, msg = "保存成功" });
        }
        [HttpGet]
        public IActionResult AddPermission(string roleId)
        {
            ViewBag.RoleId = roleId;
            //所有权限
            var permissions = PermissionService.GetAll();
            var pmsList = from f in permissions
                          select new
                          {
                              value = f.Id,
                              title = f.Name
                          };
            ViewBag.Permissions = JsonConvert.SerializeObject(pmsList);
            //当前角色下的权限
            var rolesPermissions = PermissionService.GetByRoleId(long.Parse(roleId));
            var rolePmsList = rolesPermissions.Select(p => p.Id);
            ViewBag.RolePms = JsonConvert.SerializeObject(rolePmsList);
            return View();
        }
        [HttpPost]
        public IActionResult AddPermission(string roleId, string getData)
        {

            var data = JsonConvert.DeserializeObject<List<JsonModel>>(getData);
            //从集合中筛选出value的值
            long[] pmsIds = data.Select(p => p.Value).ToArray();
            PermissionService.AddPermIds(long.Parse(roleId), pmsIds);
            return Json(new Result { Code = 0, Msg = "保存成功" });
        }

    }
}