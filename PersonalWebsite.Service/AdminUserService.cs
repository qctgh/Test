﻿using Microsoft.EntityFrameworkCore;
using PersonalWebsite.DTO;
using PersonalWebsite.Helper;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class AdminUserService : IAdminUserService
    {
        private readonly MyDbContext ctx;
        public AdminUserService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }

        public long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId)
        {
            AdminUserEntity user = new AdminUserEntity();
            user.Email = email;
            user.Name = name;
            user.PhoneNum = phoneNum;
            string salt = CommonHelper.CreateVerifyCode(5);//盐
            user.PasswordSalt = salt;
            //Md5(盐+用户密码)
            string pwdHash = CommonHelper.CalcMD5(salt + password);
            user.PasswordHash = pwdHash;

            bool exists = ctx.AdminUsers.Any(u => u.PhoneNum == phoneNum);
            if (exists)
            {
                throw new ArgumentException("手机号已经存在" + phoneNum);
            }
            ctx.AdminUsers.Add(user);
            ctx.SaveChanges();
            return user.Id;

        }

        public bool CheckLogin(string phoneNum, string password)
        {

            //出了错不可怕，最怕的是有错但是表面“风平浪静”
            var user = ctx.AdminUsers.SingleOrDefault(u => u.PhoneNum == phoneNum);
            if (user == null)
            {
                return false;
            }
            string dbHash = user.PasswordHash;
            string userHash = CommonHelper.CalcMD5(user.PasswordSalt + password);
            //比较数据库中的PasswordHash是否和MD5(salt+用户输入密码)一直
            return userHash == dbHash;

        }

        private AdminUserDTO ToDTO(AdminUserEntity user)
        {
            AdminUserDTO dto = new AdminUserDTO();
            dto.CreateDateTime = user.CreateDateTime;
            dto.Email = user.Email;
            dto.Id = user.Id;
            dto.LastLoginErrorDateTime = user.LastLoginErrorDateTime;
            dto.LoginErrorTimes = user.LoginErrorTimes;
            dto.Name = user.Name;
            dto.PhoneNum = user.PhoneNum;
            dto.IsDeleted = user.IsDeleted ? "是" : "否";
            dto.DeletedDateTime = user.DeletedDateTime;
            return dto;
        }

        public AdminUserDTO[] GetAll()
        {

            //using System.Data.Entity;才能在IQueryable中用Include、AsNoTracking
            return ctx.AdminUsers.AsNoTracking().ToList().Select(u => ToDTO(u)).ToArray();

        }

        public AdminUserDTO GetById(long id)
        {
            //这里不能用bs.GetById(id);因为无法Include、AsNoTracking()等
            var user = ctx.AdminUsers
                .AsNoTracking().SingleOrDefault(u => u.Id == id);
            //.AsNoTracking().Where(u=>u.Id==id).SingleOrDefault();
            //var user = bs.GetById(id); 用include就不能用GetById
            if (user == null)
            {
                return null;
            }
            return ToDTO(user);

        }

        public AdminUserDTO GetByPhoneNum(string phoneNum)
        {


            var users = ctx.AdminUsers.AsNoTracking().Where(u => u.PhoneNum == phoneNum);
            int count = users.Count();
            if (count <= 0)
            {
                return null;
            }
            else if (count == 1)
            {
                return ToDTO(users.Single());
            }
            else
            {
                throw new ApplicationException("找到多个手机号为" + phoneNum + "的管理员");
            }

        }

        //HasPermission(5,"User.Add")
        public bool HasPermission(long adminUserId, string permissionName)
        {
            var user = ctx.AdminUsers.AsNoTracking().SingleOrDefault(u => u.Id == adminUserId);
            //var user = bs.GetById(adminUserId);
            if (user == null)
            {
                throw new ArgumentException("找不到id=" + adminUserId + "的用户");
            }
            //每个Role都有一个Permissions属性
            //Roles.SelectMany(r => r.Permissions)就是遍历Roles的每一个Role
            //然后把每个Role的Permissions放到一个集合中
            //IEnumerable<PermissionEntity>
            //return user.Roles.SelectMany(r => r.Permissions).Any(p => p.Name == permissionName);
            return user.AdminUserRoles.SelectMany(p => p.Role.RolesPermissions).Any(p => p.Permission.Name == permissionName);

        }

        public void MarkDeleted(long adminUserId)
        {

            var adminUser = ctx.AdminUsers.SingleOrDefault(p => p.Id.Equals(adminUserId));
            adminUser.IsDeleted = true;
            ctx.SaveChanges();

        }

        public void RecordLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void ResetLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdminUser(long id, string name, string phoneNum,
            string password, string email, long? cityId)
        {

            var user = ctx.AdminUsers.SingleOrDefault(p => p.Id.Equals(id));
            if (user == null)
            {
                throw new ArgumentException("找不到id=" + id + "的管理员");
            }
            user.Name = name;
            user.PhoneNum = phoneNum;
            user.Email = email;
            if (!string.IsNullOrEmpty(password))
            {
                user.PasswordHash =
                CommonHelper.CalcMD5(user.PasswordSalt + password);
            }
            ctx.SaveChanges();

        }
    }
}
