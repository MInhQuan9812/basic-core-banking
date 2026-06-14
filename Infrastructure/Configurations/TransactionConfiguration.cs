using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable(nameof(Transaction));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.BalanceAfter)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.ReferenceId)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.ReferenceId)
                .IsUnique()
                .HasDatabaseName("IX_Transaction_ReferenceId_Unique");

            builder.Property(x => x.ReferenceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasIndex(x => x.ReferenceNumber)
                .IsUnique()
                .HasFilter("reference_number IS NOT NULL")
                .HasDatabaseName("IX_Transaction_ReferenceNumber_Unique");


            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.RelatedAccount)
                .WithMany()
                .HasForeignKey(x => x.RelatedAccountId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
