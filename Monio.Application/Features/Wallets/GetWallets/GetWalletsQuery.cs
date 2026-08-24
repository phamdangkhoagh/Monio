using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Monio.Application.Features.Wallets.GetWallets
{
    public class GetWalletsQuery : IRequest<List<WalletResponse>>
    {

    }
}
