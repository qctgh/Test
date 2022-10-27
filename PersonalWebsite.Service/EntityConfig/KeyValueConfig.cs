using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class KeyValueConfig : IEntityTypeConfiguration<KeyValueEntity>
    {
        public void Configure(EntityTypeBuilder<KeyValueEntity> builder)
        {
            builder.ToTable("T_KeyValues");
            builder.Property(p => p.Key).IsRequired().HasMaxLength(1024);
            builder.Property(p => p.Value).IsRequired().HasMaxLength(1024);
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
