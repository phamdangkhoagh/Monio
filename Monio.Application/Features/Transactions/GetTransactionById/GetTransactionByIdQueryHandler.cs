using MediatR;
using Monio.Application.Features.Transactions.CreateTransaction;
using Monio.Application.Features.Transactions.GetTransactions;
using Monio.Application.Interfaces.Services;
using Monio.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Transactions.GetTransactionById
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionResponse>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTransactionByIdQueryHandler(
            ITransactionRepository transactionRepository,
            ICurrentUserService currentUserService)
        {
            _transactionRepository = transactionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<TransactionResponse> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var transaction = await _transactionRepository
                .GetByIdForUserAsync(request.Id, userId);

            if (transaction == null)
                throw new KeyNotFoundException("Transaction not found.");

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
            };
        }
    }
}
