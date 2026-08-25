using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Transactions.CreateTransaction
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = default!;
        public decimal ExchangeRate { get; set; }
        public TransactionType Type { get; set; }
        public string? Note { get; set; }
        public string? ImageUrl { get; set; }
        public DateOnly TransactionDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
