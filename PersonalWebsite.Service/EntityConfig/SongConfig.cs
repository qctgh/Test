using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntityConfig
{
    public class SongConfig : IEntityTypeConfiguration<SongEntity>
    {
        public void Configure(EntityTypeBuilder<SongEntity> builder)
        {
            builder.ToTable("T_Songs");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Artist).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Album).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Mp3).IsRequired().HasMaxLength(300);
            builder.HasQueryFilter(p => !p.IsDeleted);
            builder.HasOne(p => p.SongMenu).WithMany(p => p.Songs).HasForeignKey(p => p.SongMenuId);
        }
    }
}
