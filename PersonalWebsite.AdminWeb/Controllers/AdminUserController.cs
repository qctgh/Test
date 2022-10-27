using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class AdminUserController : Controller
    {
        protected IAdminUserService AdminUserService { get; set; }
        protected IRoleService RoleService { get; set; }

        public AdminUserController(IAdminUserService AdminUserService, IRoleService RoleService)
        {
            this.AdminUserService = AdminUserService;
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
        public IActionResult List(string str)
        {
            var adminUsers = AdminUserService.GetAll();
            Result result = new Result();
            result.Code = 0;
            result.Data = adminUsers;
            result.Count = adminUsers.Length;
            result.Msg = "";
            return Json(result);
        }
        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Add(AdminUserAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new FormResult { status = 1, msg = "数据验证未通过" });
            }
            //服务器端的校验必不可少
            bool exists = AdminUserService.GetByPhoneNum(model.PhoneNum) != null;
            if (exists)
            {
                return Json(new FormResult { status = 1, msg = "手机号已经存在" });
            }

            long userId = AdminUserService.AddAdminUser(model.Name, model.PhoneNum, model.Password, model.Email, null);
            return Json(new FormResult { status = 0, msg = "保存成功" });
        }
        [HttpGet]
        public IActionResult AddRole(string adminUserId)
        {
            ViewBag.AdminUserId = adminUserId;
            //所有角色
            var roles = RoleService.GetAll();
            var roleList = from f in roles
                           select new
                           {
                               value = f.Id,
                               title = f.Name
                           };
            ViewBag.Roles = JsonConvert.SerializeObject(roleList);
            //当前用户下的角色
            var adminUserRoles = RoleService.GetByAdminUserId(long.Parse(adminUserId));
            var adminUserRole = adminUserRoles.Select(p => p.Id);
            ViewBag.AdminUserRole = JsonConvert.SerializeObject(adminUserRole);
            return View();
        }
        [HttpPost]
        public IActionResult AddRole(string adminUserId, string getData)
        {
            var data = JsonConvert.DeserializeObject<List<JsonModel>>(getData);
            //从集合中筛选出value的值
            long[] roleIds = data.Select(p => p.Value).ToArray();
            RoleService.AddRoleIds(long.Parse(adminUserId), roleIds);
            return Json(new Result { Code = 0, Msg = "保存成功" });
        }


    }
}