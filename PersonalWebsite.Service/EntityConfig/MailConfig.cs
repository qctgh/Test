using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class MailConfig : IEntityTypeConfiguration<MailEntity>
    {
        public void Configure(EntityTypeBuilder<MailEntity> builder)
        {
            builder.ToTable("T_Mails");
            builder.Property(p => p.Type).IsRequired().HasMaxLength(10);
            builder.Property(p => p.WorkId);
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.FromEmail).IsRequired();
            builder.Property(p => p.ToEmail).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
