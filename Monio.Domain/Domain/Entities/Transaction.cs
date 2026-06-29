using Monio.Domain.Enums;

namespace Monio.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid WalletId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid? RecurringTransactionId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "VND";
    public decimal ExchangeRate { get; set; } = 1;
    public TransactionType Type { get; set; }
    public string? Note { get; set; }
    public string? ImageUrl { get; set; }
    public DateOnly TransactionDate { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation
    public User User { get; set; } = default!;
    public Wallet Wallet { get; set; } = default!;
    public Category Category { get; set; } = default!;
    public RecurringTransaction? RecurringTransaction { get; set; }
}
