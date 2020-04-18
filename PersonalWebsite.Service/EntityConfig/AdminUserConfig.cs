using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class AdminUserConfig : IEntityTypeConfiguration<AdminUserEntity>
    {
        public void Configure(EntityTypeBuilder<AdminUserEntity> builder)
        {
            builder.ToTable("T_AdminUsers");
            //builder.HasOne(u => u.Roles).WithMany(r => r.AdminUsers).Map(m => m.ToTable("T_AdminUserRoles")
            //    .MapLeftKey("AdminUserId").MapRightKey("RoleId"));
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(30).IsRequired().IsUnicode(false);//varchar(30)
            builder.Property(p => p.PhoneNum).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.Property(p => p.PasswordSalt).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.Property(p => p.PasswordHash).HasMaxLength(100).IsRequired().IsUnicode(false);
            builder.Ignore(p => p.Roles);
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
