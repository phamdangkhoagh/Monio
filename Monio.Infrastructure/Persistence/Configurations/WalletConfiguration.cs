using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monio.Domain.Entities;

namespace Monio.Infrastructure.Persistence.Configurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets");

        builder.HasKey(w => w.Id);
        builder.Property(w => w.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(w => w.Name).IsRequired().HasMaxLength(100);
        builder.Property(w => w.Type).IsRequired()
            .HasConversion<string>().HasMaxLength(20);
        builder.Property(w => w.Currency).IsRequired().HasMaxLength(3).HasDefaultValue("VND");
        builder.Property(w => w.IsActive).HasDefaultValue(true);
        builder.Property(w => w.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(w => w.User)
            .WithMany(u => u.Wallets)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(w => w.UserId);
    }
}
