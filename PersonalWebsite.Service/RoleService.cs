using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;

namespace PersonalWebsite.Service
{
    public class RoleService : IRoleService
    {
        public long AddNew(string roleName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                bool exists = ctx.Roles.Any(r => r.Name == roleName);
                //正常情况不应该执行这个异常，因为UI层应该把这些情况处理好
                //这里只是“把好最后一关”
                if (exists)
                {
                    throw new ArgumentException("角色名字已经存在" + roleName);
                }
                RoleEntity role = new RoleEntity();
                role.Name = roleName;
                ctx.Roles.Add(role);
                ctx.SaveChanges();
                return role.Id;
            }
        }

        public void AddRoleIds(long adminUserId, long[] roleIds)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var user = ctx.AdminUsers.SingleOrDefault(p => p.Id.Equals(adminUserId));
                if (user == null)
                {
                    throw new ArgumentException("用户不存在" + adminUserId);
                }
                //删除已有的角色
                var adminUserRoles = ctx.AdminUserRoles.Where(p => p.AdminUserId == adminUserId);
                ctx.RemoveRange(adminUserRoles);
                //重新添加
                var roles = ctx.Roles.Where(p => roleIds.Contains(p.Id)).ToList();
                foreach (var item in roles)
                {
                    user.AdminUserRoles.Add(new AdminUserRolesEntity { AdminUserId = adminUserId, RoleId = item.Id });
                }

                ctx.SaveChanges();
            }
        }

        private RoleDTO ToDTO(RoleEntity en)
        {
            RoleDTO dto = new RoleDTO();
            dto.CreateDateTime = en.CreateDateTime;
            dto.Id = en.Id;
            dto.Name = en.Name;
            dto.IsDeleted = en.IsDeleted ? "是" : "否";
            dto.DeletedDateTime = en.DeletedDateTime;
            return dto;
        }

        public RoleDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                return ctx.Roles.AsNoTracking().ToList().Select(p => ToDTO(p)).ToArray();
            }
        }

        public RoleDTO[] GetByAdminUserId(long adminUserId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var user = ctx.AdminUsers.SingleOrDefault(p => p.Id == adminUserId);
                if (user == null)
                {
                    throw new ArgumentException("不存在的管理员" + adminUserId);
                }
                //var adminRoles = ctx.AdminUserRoles.Where(p => p.AdminUserId == adminUserId).ToList();
                //var roleIds = adminRoles.Select(p => p.RoleId);
                //return ctx.Roles.Where(p => roleIds.Contains(p.Id)).Select(p => ToDTO(p)).ToArray();
                var adminRoles = ctx.AdminUsers.Include(p => p.AdminUserRoles).SingleOrDefault(p => p.Id == adminUserId).AdminUserRoles.ToList();
                var roleIds = adminRoles.Select(p => p.RoleId);
                return ctx.Roles.Where(p => roleIds.Contains(p.Id)).Select(p => ToDTO(p)).ToArray();
            }
        }

        public RoleDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var role = ctx.Roles.SingleOrDefault(p => p.Id.Equals(id));
                return role == null ? null : ToDTO(role);
            }
        }

        public RoleDTO GetByName(string name)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var role = ctx.Roles.SingleOrDefault(r => r.Name == name);
                return role == null ? null : ToDTO(role);
            }
        }

        public void MarkDeleted(long roleId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var role = ctx.Roles.SingleOrDefault(p => p.Id.Equals(roleId));
                role.IsDeleted = true;
                ctx.SaveChanges();
            }
        }

        public void Update(long roleId, string roleName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                bool exists = ctx.Roles.Any(r => r.Name == roleName && r.Id != roleId);
                //正常情况不应该执行这个异常，因为UI层应该把这些情况处理好
                //这里只是“把好最后一关”
                if (exists)
                {
                    throw new ArgumentException("");
                }
                RoleEntity role = new RoleEntity();
                role.Id = roleId;
                //ctx.Entry(role).State = System.Data.Entity.EntityState.Unchanged;
                role.Name = roleName;
                ctx.SaveChanges();
            }
        }

        public void UpdateRoleIds(long adminUserId, long[] roleIds)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var user = ctx.AdminUsers.SingleOrDefault(p => p.Id.Equals(adminUserId));
                if (user == null)
                {
                    throw new ArgumentException("用户不存在" + adminUserId);
                }
                user.AdminUserRoles.Clear();
                var roles = ctx.Roles.Where(r => roleIds.Contains(r.Id)).ToArray();
                foreach (var role in roles)
                {
                    //user.Roles.Add(role);
                }
                ctx.SaveChanges();
            }
        }
    }
}
