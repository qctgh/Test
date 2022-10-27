using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntityConfig
{
    public class SysUpdateLogConfig : IEntityTypeConfiguration<SysUpdateLogEntity>
    {
        public void Configure(EntityTypeBuilder<SysUpdateLogEntity> builder)
        {
            builder.ToTable("T_SysUpdateLogs");
            builder.Property(p => p.Content).IsRequired();
        }
    }
}
