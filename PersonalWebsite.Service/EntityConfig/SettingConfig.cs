using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class SettitngConfig : IEntityTypeConfiguration<SettingEntity>
    {
        public void Configure(EntityTypeBuilder<SettingEntity> builder)
        {
            builder.ToTable("T_Settings");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(1024);
            builder.Property(p => p.Value).IsRequired();
        }
    }
}
