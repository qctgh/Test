using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntityConfig
{
    public class CommentConfig : IEntityTypeConfiguration<CommentEntity>
    {
        public void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.ToTable("T_Comments");
            builder.Property(p => p.Content).HasMaxLength(500).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
