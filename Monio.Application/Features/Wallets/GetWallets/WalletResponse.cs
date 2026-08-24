using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Wallets.GetWallets
{
    public class WalletResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public WalletType Type { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
