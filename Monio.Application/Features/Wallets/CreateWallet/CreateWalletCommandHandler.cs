using MediatR;
using Monio.Application.Features.Wallets.GetWallets;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Wallets.CreateWallet
{
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, WalletResponse>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateWalletCommandHandler(
            IWalletRepository walletRepository,
            ICurrentUserService currentUserService)
        {
            _walletRepository = walletRepository;
            _currentUserService = currentUserService;
        }

        public async Task<WalletResponse> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                UserId = _currentUserService.UserId,
                Name = request.Name,
                Type = request.Type,
                Currency = request.Currency
            };

            await _walletRepository.AddAsync(wallet);

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
