using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;

namespace PersonalWebsite.Service.EntityConfig
{
    public class ArticleRateConfig : IEntityTypeConfiguration<ArticleRateEntity>
    {
        public void Configure(EntityTypeBuilder<ArticleRateEntity> builder)
        {
            builder.ToTable("T_ArticleRates");
            builder.Property(p => p.ArticleId).IsRequired();
            builder.Property(p => p.IP).IsRequired().HasMaxLength(20);
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
