using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class RolePermissionsConfig : IEntityTypeConfiguration<RolePermissionsEntity>
    {
        public void Configure(EntityTypeBuilder<RolePermissionsEntity> builder)
        {
            builder.ToTable("T_RolePermissions");

            builder.HasOne(p => p.Role).WithMany(p => p.RolesPermissions).HasForeignKey(p => p.RoleId);

            builder.HasOne(p => p.Permission).WithMany(p => p.RolePermissions).HasForeignKey(p => p.PermissionId);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
