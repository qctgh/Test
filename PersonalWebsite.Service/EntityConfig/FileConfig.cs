using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;

namespace PersonalWebsite.Service.EntityConfig
{
    public class FileConfig : IEntityTypeConfiguration<FileEntity>
    {
        public void Configure(EntityTypeBuilder<FileEntity> builder)
        {
            builder.ToTable("T_Files");
            builder.Property(p => p.Guid).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Hash).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Type).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Size).HasMaxLength(50).IsRequired();
            builder.Property(p => p.UserId).HasMaxLength(50).IsRequired();

        }
    }
}
