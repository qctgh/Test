using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;

namespace PersonalWebsite.Service.EntityConfig
{
    public class MessageConfig : IEntityTypeConfiguration<MessageEntity>
    {
        public void Configure(EntityTypeBuilder<MessageEntity> builder)
        {
            builder.ToTable("T_Messages");
            builder.Property(p => p.Content).HasMaxLength(500).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
