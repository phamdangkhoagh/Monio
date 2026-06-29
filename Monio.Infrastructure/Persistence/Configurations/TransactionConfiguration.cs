using Monio.Domain.Entities;                                 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Monio.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(t => t.Amount).IsRequired().HasPrecision(18, 2);
        builder.Property(t => t.Currency).IsRequired().HasMaxLength(3).HasDefaultValue("VND");
        builder.Property(t => t.ExchangeRate).IsRequired().HasPrecision(18, 6).HasDefaultValue(1m);
        builder.Property(t => t.Type).IsRequired()
            .HasConversion<string>().HasMaxLength(10);
        builder.Property(t => t.Note).HasMaxLength(500);
        builder.Property(t => t.ImageUrl).HasMaxLength(500);
        builder.Property(t => t.TransactionDate).IsRequired();
        builder.Property(t => t.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Wallet)
            .WithMany(w => w.Transactions)
            .HasForeignKey(t => t.WalletId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.RecurringTransaction)
            .WithMany(r => r.Transactions)
            .HasForeignKey(t => t.RecurringTransactionId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        // Composite indexes cho các query phổ biến nhất
        builder.HasIndex(t => new { t.UserId, t.TransactionDate });
        builder.HasIndex(t => new { t.UserId, t.CategoryId });
        builder.HasIndex(t => t.WalletId);
        builder.HasIndex(t => t.RecurringTransactionId);
    }
}
