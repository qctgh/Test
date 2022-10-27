using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.EntitiesConfig
{
    public class UserConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("T_Users");
            builder.Property(p => p.PasswordHash).HasMaxLength(100);
            builder.Property(p => p.PasswordSalt).HasMaxLength(20);
            builder.Property(p => p.PhoneNum).HasMaxLength(20).IsUnicode(false);
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
