using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(500);
        builder.Property(u => u.AvatarUrl).HasMaxLength(500);
        builder.Property(u => u.DefaultCurrency).IsRequired().HasMaxLength(3).HasDefaultValue("VND");

        builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(u => u.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(u => u.Email).IsUnique();
    }
}
