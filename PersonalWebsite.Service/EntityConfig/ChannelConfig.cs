
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntityConfig
{
    public class ChannelConfig : IEntityTypeConfiguration<ChannelEntity>
    {
        public void Configure(EntityTypeBuilder<ChannelEntity> builder)
        {
            builder.ToTable("T_Channels");
            builder.Property(p => p.ParentId).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
