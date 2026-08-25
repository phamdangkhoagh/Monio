using MediatR;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using Monio.Domain.Entities;
using Monio.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Transactions.CreateTransaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionResponse>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransactionCommandHandler(
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

        public async Task<TransactionResponse> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var wallet = await _walletRepository
                .GetByIdAndUserIdAsync(request.WalletId, userId);

            if (wallet == null)
                throw new Exception("Wallet not found.");

            var category = await _categoryRepository
                .GetForTransactionAsync(request.CategoryId, userId);

            if (category == null)
                throw new Exception("Category not found.");

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                WalletId = request.WalletId,
                CategoryId = request.CategoryId,
                //RecurringTransactionId = request.RecurringTransactionId,
                Amount = request.Amount,
                Currency = request.Currency,
                ExchangeRate = request.ExchangeRate,
                Type = request.Type,
                Note = request.Note,
                ImageUrl = request.ImageUrl,
                TransactionDate = request.TransactionDate,
                CreatedAt = DateTime.UtcNow,
            };

            // Update wallet balance
            if (request.Type == TransactionType.Income)
            {
                wallet.Balance += request.Amount;
            } else if (request.Type == TransactionType.Expense)
            {
                wallet.Balance -= request.Amount;
            }

            await _transactionRepository.AddAsync(transaction);
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
                CreatedAt = transaction.CreatedAt
            };
        }
    }
}
