using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;

namespace PersonalWebsite.Service.EntityConfig
{
    public class StageConfig : IEntityTypeConfiguration<StageEntity>
    {
        public void Configure(EntityTypeBuilder<StageEntity> builder)
        {
            builder.ToTable("T_Stages");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
