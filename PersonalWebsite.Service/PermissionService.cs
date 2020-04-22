using PersonalWebsite.IService;
using System;
using System.Linq;
using PersonalWebsite.DTO;
using PersonalWebsite.Service.Entity;
using PersonalWebsite.Service;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ZSZ.Service
{
    public class PermissionService : IPermissionService
    {
        public long AddPermission(string permName, string description)
        {
            using (MyDbContext ctx = new MyDbContext())
            {

                bool exists = ctx.Permissions.Any(p => p.Name == permName);
                if (exists)
                {
                    throw new ArgumentException("权限项已经存在");
                }
                PermissionEntity perm = new PermissionEntity();
                perm.Description = description;
                perm.Name = permName;
                ctx.Permissions.Add(perm);
                ctx.SaveChanges();
                return perm.Id;
            }
        }

        public void AddPermIds(long roleId, long[] permIds)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var role = ctx.Roles.SingleOrDefault(p => p.Id.Equals(roleId));
                if (role == null)
                {
                    throw new ArgumentException("roleId不存在" + roleId);
                }
                var rolePms = ctx.RolePermissions.Where(p => p.RoleId == roleId);
                ctx.RemoveRange(rolePms);

                var perms = ctx.Permissions.Where(p => permIds.Contains(p.Id)).ToArray();
                foreach (var perm in perms)
                {
                    role.RolesPermissions.Add(new RolePermissionsEntity { RoleId = roleId, PermissionId = perm.Id });
                }
                ctx.SaveChanges();
            }
        }

        private PermissionDTO ToDTO(PermissionEntity p)
        {
            PermissionDTO dto = new PermissionDTO();
            dto.CreateDateTime = p.CreateDateTime;
            dto.Description = p.Description;
            dto.Id = p.Id;
            dto.Name = p.Name;
            dto.IsDeleted = p.IsDeleted ? "是" : "否";
            return dto;
        }

        public PermissionDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                return ctx.Permissions.Select(p => ToDTO(p)).ToArray();
            }
        }

        public PermissionDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var pe = ctx.Permissions.SingleOrDefault(p => p.Id.Equals(id));
                return pe == null ? null : ToDTO(pe);
            }
        }

        public PermissionDTO GetByName(string name)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var pe = ctx.Permissions.SingleOrDefault(p => p.Name == name);
                return pe == null ? null : ToDTO(pe);
            }
        }

        public PermissionDTO[] GetByRoleId(long roleId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var permissions = ctx.RolePermissions.Where(p => p.RoleId == roleId).ToList();
                var permissionIds = permissions.Select(p => p.PermissionId);
                return ctx.Permissions.Where(p => permissionIds.Contains(p.Id)).ToList().Select(p => ToDTO(p)).ToArray();
            }
        }

        //2,3,4
        //3,4,5
        public void UpdatePermIds(long roleId, long[] permIds)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                PermissionEntity role = ctx.Permissions.SingleOrDefault(p => p.Id.Equals(roleId));
                if (role == null)
                {
                    throw new ArgumentException("roleId不存在" + roleId);
                }
                role.RolePermissions.Clear();

                //todo:
                //var perms = ctx.ro.Where(p => permIds.Contains(p.Id)).ToList();
                //foreach (var perm in perms)
                //{
                //    role.RolePermissions.Add(perm);
                //}
                ctx.SaveChanges();
            }
        }

        public void UpdatePermission(long id, string permName, string description)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var perm = ctx.Permissions.SingleOrDefault(p => p.Id.Equals(id));
                if (perm == null)
                {
                    throw new ArgumentException("id不存在" + id);
                }
                perm.Name = permName;
                perm.Description = description;
                ctx.SaveChanges();
            }
        }

        public void MarkDeleted(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var permission = ctx.Permissions.SingleOrDefault(p => p.Id.Equals(id));
                permission.IsDeleted = true;
                ctx.SaveChanges();
            }
        }
    }
}
