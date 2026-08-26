using MediatR;
using Monio.Application.Features.Transactions.CreateTransaction;
using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Transactions.UpdateTransaction
{
    public class UpdateTransactionCommand : IRequest<TransactionResponse>
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public Guid CategoryId { get; set; }
        //public Guid? RecurringTransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "VND";
        public decimal ExchangeRate { get; set; } = 1;
        public TransactionType Type { get; set; }
        public string? Note { get; set; }
        public string? ImageUrl { get; set; }
        public DateOnly TransactionDate { get; set; }
    }
}
