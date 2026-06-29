using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RecurringTransactionConfiguration : IEntityTypeConfiguration<RecurringTransaction>
{
    public void Configure(EntityTypeBuilder<RecurringTransaction> builder)
    {
        builder.ToTable("RecurringTransactions",
            t => t.HasCheckConstraint("CK_RecurringTransactions_Amount", "[Amount] > 0"));

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(r => r.Amount).IsRequired().HasPrecision(18, 2);
        builder.Property(r => r.Currency).IsRequired().HasMaxLength(3).HasDefaultValue("VND");
        builder.Property(r => r.Type).IsRequired()
            .HasConversion<string>().HasMaxLength(10);
        builder.Property(r => r.Frequency).IsRequired()
            .HasConversion<string>().HasMaxLength(10);
        builder.Property(r => r.NextRunDate).IsRequired();
        builder.Property(r => r.IsActive).HasDefaultValue(true);
        builder.Property(r => r.Note).HasMaxLength(500);

        builder.HasOne(r => r.User)
            .WithMany(u => u.RecurringTransactions)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Wallet)
            .WithMany()
            .HasForeignKey(r => r.WalletId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Category)
            .WithMany(c => c.RecurringTransactions)
            .HasForeignKey(r => r.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Partial index: cron job chỉ scan các recurring đang active
        builder.HasIndex(r => r.NextRunDate)
            .HasFilter("[IsActive] = 1");
    }
}
