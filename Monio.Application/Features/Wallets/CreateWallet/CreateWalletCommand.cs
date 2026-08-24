using MediatR;
using Monio.Application.Features.Wallets.GetWallets;
using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Wallets.CreateWallet
{
    public class CreateWalletCommand : IRequest<WalletResponse>
    {
        public string Name { get; set; } = default!;
        public WalletType Type { get; set; }
        public string Currency { get; set; } = "VND";
    }
}
