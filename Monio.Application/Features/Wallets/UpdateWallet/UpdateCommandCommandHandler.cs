using MediatR;
using Monio.Application.Features.Wallets.GetWallets;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Wallets.UpdateWallet
{
    public class UpdateCommandCommandHandler : IRequestHandler<UpdateWalletCommand, WalletResponse>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCommandCommandHandler(
            IWalletRepository walletRepository,
            ICurrentUserService currentUserService)
        {
            _walletRepository = walletRepository;
            _currentUserService = currentUserService;
        }

        public async Task<WalletResponse> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var wallet = await _walletRepository.GetByIdAndUserIdAsync(request.Id, userId);

            if (wallet == null) 
            {
                throw new Exception("Wallet not found.");
            }

            wallet.Name = request.Name;
            wallet.Type = request.Type;
            wallet.Currency = request.Currency;
            wallet.IsActive = request.IsActive;

            await _walletRepository.UpdateAsync(wallet);

            return new WalletResponse
            {
                Id = wallet.Id,
                Name = wallet.Name,
                Type = wallet.Type,
                Balance = wallet.Balance,
                Currency = wallet.Currency,
                IsActive = wallet.IsActive
            };
        }
    }
}
