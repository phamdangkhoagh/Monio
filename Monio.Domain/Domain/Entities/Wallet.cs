using Monio.Domain.Enums;

namespace Monio.Domain.Entities;

public class Wallet
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public WalletType Type { get; set; }
    public string Currency { get; set; } = "VND";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    // Navigation
    public User User { get; set; } = default!;
    public ICollection<Transaction> Transactions { get; set; } = [];
}
