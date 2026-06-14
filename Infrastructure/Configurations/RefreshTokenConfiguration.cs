using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable(nameof(RefreshToken));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RefreshTokenValue).IsRequired().HasMaxLength(200).IsUnicode(false);
            builder.Property(x => x.ExpiresAt).IsRequired();
            builder.Property(x => x.IsRevoked).IsRequired();

            builder.HasIndex(x => x.RefreshTokenValue).IsUnique()
                .HasDatabaseName("IX_RefreshToken_Value_Unique");

            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<RefreshToken>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
