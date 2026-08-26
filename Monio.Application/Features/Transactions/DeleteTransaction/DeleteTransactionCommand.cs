using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Transactions.DeleteTransaction
{
    public class DeleteTransactionCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
