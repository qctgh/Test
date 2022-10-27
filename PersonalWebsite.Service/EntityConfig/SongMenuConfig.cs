using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;

namespace PersonalWebsite.Service.EntityConfig
{
    public class SongMenuConfig : IEntityTypeConfiguration<SongMenuEntity>
    {
        public void Configure(EntityTypeBuilder<SongMenuEntity> builder)
        {
            builder.ToTable("T_SongMenus");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Tags).HasMaxLength(200);
            builder.Property(p => p.CoverImgSrc).IsRequired().HasMaxLength(300);
            builder.HasQueryFilter(p => !p.IsDeleted);
            builder.HasMany(p => p.Songs).WithOne(p => p.SongMenu).HasForeignKey(p => p.SongMenuId);
        }
    }
}
