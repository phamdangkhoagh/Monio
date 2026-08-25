using MediatR;
using Monio.Application.Features.Transactions.CreateTransaction;
using Monio.Application.Interfaces.Services;
using Monio.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Transactions.GetTransactions
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionResponse>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTransactionsQueryHandler(
            ITransactionRepository transactionRepository,
            ICurrentUserService currentUserService)
        {
            _transactionRepository = transactionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<TransactionResponse>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var transactions = await _transactionRepository.GetForUserAsync(
                userId,
                request.WalletId,
                request.CategoryId,
                request.Type,
                request.FromDate,
                request.ToDate,
                request.Page,
                request.PageSize);

            return transactions.Select(transaction => new TransactionResponse
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
            }).ToList();

        }
    }
}
