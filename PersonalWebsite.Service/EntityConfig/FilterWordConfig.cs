using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntityConfig
{
    public class FilterWordConfig : IEntityTypeConfiguration<FilterWordEntity>
    {
        public void Configure(EntityTypeBuilder<FilterWordEntity> builder)
        {
            builder.ToTable("T_FilterWords");
            builder.Property(p => p.WordPattern).HasMaxLength(500).IsRequired();
            builder.Property(p => p.ReplaceWord).HasMaxLength(500).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
