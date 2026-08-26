using MediatR;
using Monio.Application.Features.Transactions.CreateTransaction;
using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Transactions.GetTransactionById
{
    public class GetTransactionByIdQuery : IRequest<TransactionResponse>
    {
        public Guid Id { get; set; }
    }
}
