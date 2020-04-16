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
            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Email).HasMaxLength(30).IsRequired().IsUnicode(false);//varchar(30)
            builder.Property(e => e.PhoneNum).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.Property(e => e.PasswordSalt).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.Property(e => e.PasswordHash).HasMaxLength(100).IsRequired().IsUnicode(false);
            builder.Ignore(e => e.Roles);
        }
    }
}
