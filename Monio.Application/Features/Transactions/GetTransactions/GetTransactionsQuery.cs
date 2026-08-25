using MediatR;
using Monio.Application.Features.Transactions.CreateTransaction;
using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Transactions.GetTransactions
{
    public class GetTransactionsQuery : IRequest<List<TransactionResponse>>
    {
        public Guid? WalletId { get; set; }
        public Guid? CategoryId { get; set; }
        public TransactionType? Type { get; set; }

        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
