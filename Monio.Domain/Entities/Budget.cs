namespace Monio.Domain.Entities;

public class Budget
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "VND";
    public BudgetPeriodType PeriodType { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation
    public User User { get; set; } = default!;
    public Category Category { get; set; } = default!;
}
