using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class PermissionConfig : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.ToTable("T_Permissions");
            builder.Property(p => p.Description).HasMaxLength(1024);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Ignore(p => p.Roles);
            builder.HasQueryFilter(p => !p.IsDeleted);
            //使用HasOne和WithOne两个扩展方法对User表和Address表进行1-1关系配置
            //builder.HasOne(q => q.Address).WithOne(q => q.User).HasForeignKey<Address>(q => q.UserID);
        }
    }
}
