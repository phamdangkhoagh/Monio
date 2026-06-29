using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monio.Domain.Entities;
using Monio.Domain.Enums;

namespace Monio.Infrastructure.Persistence.Configurations;

/// <summary>
/// Seed các danh mục mặc định của hệ thống (UserId = null, IsSystem = true)
/// </summary>
public class CategorySeedConfiguration : IEntityTypeConfiguration<Category>
{
    // Expense categories
    private static readonly Guid FoodId       = Guid.Parse("00000000-0000-0000-0000-000000000001");
    private static readonly Guid TransportId  = Guid.Parse("00000000-0000-0000-0000-000000000002");
    private static readonly Guid ShoppingId   = Guid.Parse("00000000-0000-0000-0000-000000000003");
    private static readonly Guid EntertainId  = Guid.Parse("00000000-0000-0000-0000-000000000004");
    private static readonly Guid HealthId     = Guid.Parse("00000000-0000-0000-0000-000000000005");
    private static readonly Guid EducationId  = Guid.Parse("00000000-0000-0000-0000-000000000006");
    private static readonly Guid HousingId    = Guid.Parse("00000000-0000-0000-0000-000000000007");
    private static readonly Guid OtherExpId   = Guid.Parse("00000000-0000-0000-0000-000000000008");

    // Income categories
    private static readonly Guid SalaryId     = Guid.Parse("00000000-0000-0000-0000-000000000009");
    private static readonly Guid FreelanceId  = Guid.Parse("00000000-0000-0000-0000-000000000010");
    private static readonly Guid InvestmentId = Guid.Parse("00000000-0000-0000-0000-000000000011");
    private static readonly Guid OtherIncId   = Guid.Parse("00000000-0000-0000-0000-000000000012");

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            // === EXPENSE ===
            new Category { Id = FoodId,      Name = "Ăn uống",       Icon = "🍜", Color = "#FF6B6B", Type = CategoryType.Expense, IsSystem = true },
            new Category { Id = TransportId, Name = "Di chuyển",      Icon = "🚗", Color = "#4ECDC4", Type = CategoryType.Expense, IsSystem = true },
            new Category { Id = ShoppingId,  Name = "Mua sắm",        Icon = "🛍️", Color = "#45B7D1", Type = CategoryType.Expense, IsSystem = true },
            new Category { Id = EntertainId, Name = "Giải trí",       Icon = "🎬", Color = "#96CEB4", Type = CategoryType.Expense, IsSystem = true },
            new Category { Id = HealthId,    Name = "Sức khỏe",       Icon = "💊", Color = "#FFEAA7", Type = CategoryType.Expense, IsSystem = true },
            new Category { Id = EducationId, Name = "Giáo dục",       Icon = "📚", Color = "#DDA0DD", Type = CategoryType.Expense, IsSystem = true },
            new Category { Id = HousingId,   Name = "Nhà ở",          Icon = "🏠", Color = "#F0E68C", Type = CategoryType.Expense, IsSystem = true },
            new Category { Id = OtherExpId,  Name = "Chi khác",       Icon = "📦", Color = "#D3D3D3", Type = CategoryType.Expense, IsSystem = true },

            // === INCOME ===
            new Category { Id = SalaryId,     Name = "Lương",         Icon = "💼", Color = "#98FB98", Type = CategoryType.Income, IsSystem = true },
            new Category { Id = FreelanceId,  Name = "Freelance",     Icon = "💻", Color = "#87CEEB", Type = CategoryType.Income, IsSystem = true },
            new Category { Id = InvestmentId, Name = "Đầu tư",        Icon = "📈", Color = "#FFD700", Type = CategoryType.Income, IsSystem = true },
            new Category { Id = OtherIncId,   Name = "Thu nhập khác", Icon = "💰", Color = "#DEB887", Type = CategoryType.Income, IsSystem = true }
        );
    }
}
