using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class ArticleConfig : IEntityTypeConfiguration<ArticleEntity>
    {
        public void Configure(EntityTypeBuilder<ArticleEntity> builder)
        {
            builder.ToTable("T_Articles");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Content).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
            
            //使用HasOne和WithOne两个扩展方法对User表和Address表进行1-1关系配置
            //builder.HasOne(p => p.Address).WithOne(p => p.User).HasForeignKey<Address>(p => p.UserID);
        }
    }
}
