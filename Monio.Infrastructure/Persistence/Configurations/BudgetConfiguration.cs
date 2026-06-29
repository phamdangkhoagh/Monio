using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets",
            t => t.HasCheckConstraint("CK_Budgets_Amount", "[Amount] > 0"));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(b => b.Amount).IsRequired().HasPrecision(18, 2);
        builder.Property(b => b.Currency).IsRequired().HasMaxLength(3).HasDefaultValue("VND");
        builder.Property(b => b.PeriodType).IsRequired()
            .HasConversion<string>().HasMaxLength(10);
        builder.Property(b => b.IsActive).HasDefaultValue(true);

        // StartDate/EndDate bắt buộc khi PeriodType = Custom
        // Validate ở Application layer (FluentValidation), không enforce ở DB level
        // vì SQL Server CHECK constraint không đọc được enum string dễ dàng

        builder.HasOne(b => b.User)
            .WithMany(u => u.Budgets)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Category)
            .WithMany(c => c.Budgets)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(b => new { b.UserId, b.CategoryId, b.PeriodType });
    }
}
