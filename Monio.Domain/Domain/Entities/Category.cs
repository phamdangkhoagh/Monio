using Monio.Domain.Enums;

namespace Monio.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }   // NULL = system category
    public string Name { get; set; } = default!;
    public string? Icon { get; set; }
    public string? Color { get; set; }
    public CategoryType Type { get; set; }
    public bool IsSystem { get; set; }
    public Guid? ParentId { get; set; }

    // Navigation
    public User? User { get; set; }
    public Category? Parent { get; set; }
    public ICollection<Category> Children { get; set; } = [];
    public ICollection<Transaction> Transactions { get; set; } = [];
    public ICollection<Budget> Budgets { get; set; } = [];
    public ICollection<RecurringTransaction> RecurringTransactions { get; set; } = [];
}
