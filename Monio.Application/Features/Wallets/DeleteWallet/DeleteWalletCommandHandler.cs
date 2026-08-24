using MediatR;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Wallets.DeleteWallet
{
    public class DeleteWalletCommandHandler : IRequestHandler<DeleteWalletCommand>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteWalletCommandHandler(
            IWalletRepository walletRepository,
            ICurrentUserService currentUserService)
        {
            _walletRepository = walletRepository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var wallet = await _walletRepository.GetByIdAndUserIdAsync(request.Id, userId);

            if (wallet == null) 
            {
                throw new Exception("Wallet not found.");
            }

            await _walletRepository.DeleteAsync(wallet);
        }
    }
}
