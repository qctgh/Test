using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class TimingConfig : IEntityTypeConfiguration<TimingEntity>
    {
        public void Configure(EntityTypeBuilder<TimingEntity> builder)
        {
            builder.ToTable("T_Timings");
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.Time).IsRequired();
            builder.Property(p => p.Weeks).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
