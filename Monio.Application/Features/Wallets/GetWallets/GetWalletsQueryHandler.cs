using MediatR;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Wallets.GetWallets
{
    public class GetWalletsQueryHandler : IRequestHandler<GetWalletsQuery, List<WalletResponse>>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetWalletsQueryHandler(
            IWalletRepository walletRepository,
            ICurrentUserService currentUserService)
        {
            _walletRepository = walletRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<WalletResponse>> Handle(GetWalletsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var wallets = await _walletRepository.GetByUserIdAsync(userId);

            return wallets.Select(wallet => new WalletResponse
            {
                Id = wallet.Id,
                Name = wallet.Name,
                Type = wallet.Type,
                Balance = wallet.Balance,
                Currency = wallet.Currency,
                IsActive = wallet.IsActive
            }).ToList();
        }
    }
}
