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
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Title).HasMaxLength(200).IsRequired();
            builder.Property(q => q.Introduce).HasMaxLength(500).IsRequired();
            builder.Property(q => q.Content).IsRequired();
            builder.HasQueryFilter(q => q.IsDeleted == false);

            //使用HasOne和WithOne两个扩展方法对User表和Address表进行1-1关系配置
            //builder.HasOne(q => q.Address).WithOne(q => q.User).HasForeignKey<Address>(q => q.UserID);
        }
    }
}
