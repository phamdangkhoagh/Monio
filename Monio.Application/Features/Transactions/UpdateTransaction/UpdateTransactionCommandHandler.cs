using MediatR;
using Monio.Application.Features.Transactions.CreateTransaction;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using Monio.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Transactions.UpdateTransaction
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionResponse>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTransactionCommandHandler(
            ITransactionRepository transactionRepository,
            IWalletRepository walletRepository,
            ICategoryRepository categoryRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<TransactionResponse> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var transaction = await _transactionRepository
                .GetByIdForUserAsync(request.Id, userId);

            if (transaction == null)
                throw new KeyNotFoundException("Transaction not found.");

            var category = await _categoryRepository
                .GetForTransactionAsync(request.CategoryId, userId);

            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            var wallet = await _walletRepository
                .GetByIdAndUserIdAsync(request.WalletId, userId);

            if (wallet == null)
                throw new KeyNotFoundException("Wallet not found.");

            transaction.WalletId = request.WalletId;
            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.Currency = request.Currency;
            transaction.ExchangeRate = request.ExchangeRate;
            transaction.Type = request.Type;
            transaction.Note = request.Note;
            transaction.ImageUrl = request.ImageUrl;
            transaction.TransactionDate = request.TransactionDate;
            transaction.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return new TransactionResponse
            {
                Id = transaction.Id,
                WalletId = transaction.WalletId,
                CategoryId = transaction.CategoryId,
                Amount = transaction.Amount,
                Currency = transaction.Currency,
                ExchangeRate = transaction.ExchangeRate,
                Type = transaction.Type,
                Note = transaction.Note,
                ImageUrl = transaction.ImageUrl,
                TransactionDate = transaction.TransactionDate,
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = transaction.UpdatedAt
            };
        }
    }
}
