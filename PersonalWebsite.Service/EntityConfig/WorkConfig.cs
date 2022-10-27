using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class WorkConfig : IEntityTypeConfiguration<WorkEntity>
    {
        public void Configure(EntityTypeBuilder<WorkEntity> builder)
        {
            builder.ToTable("T_Works");
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.TimingId).IsRequired();
            builder.Property(p => p.MailId).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
