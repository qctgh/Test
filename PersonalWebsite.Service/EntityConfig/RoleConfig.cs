using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class RoleConfig : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("T_Roles");
            //EFCore不支持多对多
            //builder.HasMany(r => r.Permissions).WithMany(p => p.Roles).Map(m => m.ToTable("T_RolePermissions")
            //    .MapLeftKey("RoleId").MapRightKey("PermissionId"));
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Ignore(p => p.AdminUsers);
            builder.Ignore(p => p.Permissions);
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
