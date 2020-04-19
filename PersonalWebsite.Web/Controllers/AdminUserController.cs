using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Web.Models;

namespace PersonalWebsite.Web.Controllers
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
            var adminUsers = AdminUserService.GetAll();
            Result<AdminUserDTO> result = new Result<AdminUserDTO>();
            result.Status = 0;
            result.Data = adminUsers.ToList();
            result.Total = adminUsers.Length;
            result.Message = "";
            return Json(result);
        }
        [HttpPost]
        public IActionResult List(string str)
        {
            return View();
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
                return Json(new Result<object> { Status = 1, Message = "数据验证未通过" });
            }
            //服务器端的校验必不可少
            bool exists = AdminUserService.GetByPhoneNum(model.PhoneNum) != null;
            if (exists)
            {
                return Json(new Result<object>
                {
                    Status = 1,
                    Message = "手机号已经存在"
                });
            }

            long userId = AdminUserService.AddAdminUser(model.Name, model.PhoneNum, model.Password, model.Email, null);
            RoleService.AddRoleIds(userId, model.RoleIds);
            return Json(new Result<object> { Status = 0 });
        }


    }
}