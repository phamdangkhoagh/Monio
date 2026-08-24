using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Wallets.DeleteWallet
{
    public class DeleteWalletCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
