using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class AdminUserRolesConfig : IEntityTypeConfiguration<AdminUserRolesEntity>
    {
        public void Configure(EntityTypeBuilder<AdminUserRolesEntity> builder)
        {
            builder.ToTable("T_AdminUserRoles");

            //builder.HasKey(p => new { p.AdminUserId, p.RoleId });

            builder.HasOne(p => p.AdminUser).WithMany(p => p.AdminUserRoles).HasForeignKey(p => p.AdminUserId);
            builder.HasOne(p => p.Role).WithMany(p => p.AdminUserRoles).HasForeignKey(p => p.RoleId);
        }
    }
}
