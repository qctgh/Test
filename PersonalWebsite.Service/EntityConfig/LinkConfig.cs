using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class LinkConfig : IEntityTypeConfiguration<LinkEntity>
    {
        public void Configure(EntityTypeBuilder<LinkEntity> builder)
        {
            builder.ToTable("T_Links");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(10);
            builder.Property(p => p.Url).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Icon).IsRequired().HasMaxLength(300);
            builder.Property(p => p.Describe).IsRequired().HasMaxLength(200);
            builder.Property(p => p.OrderIndex).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
