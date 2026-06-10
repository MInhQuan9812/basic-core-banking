using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Infrastructure.Configurations
{
    public class BankDbContext(DbContextOptions<BankDbContext> options) : DbContext(options), IBankDbcontext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<AccountApprovalRequest> AccountApprovalRequests { get; set; }
        public DbSet<IdempotencyRecord> IdempotencyRecords { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TransferIntents> TransferIntents { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Here you can add any logic before saving changes, such as auditing, validation, etc.
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Configure relationships, indexes, constraints, etc. here if needed
        }

    }
}
