namespace Monio.Domain.Entities;

public class RecurringTransaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid WalletId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "VND";
    public TransactionType Type { get; set; }
    public RecurringFrequency Frequency { get; set; }
    public DateOnly NextRunDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Note { get; set; }

    // Navigation
    public User User { get; set; } = default!;
    public Wallet Wallet { get; set; } = default!;
    public Category Category { get; set; } = default!;
    public ICollection<Transaction> Transactions { get; set; } = [];
}
