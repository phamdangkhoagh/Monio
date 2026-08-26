using Microsoft.EntityFrameworkCore;
using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MonioDbContext _monioDbContext;

        public TransactionRepository(MonioDbContext monioDbContext)
        {
            _monioDbContext = monioDbContext;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _monioDbContext.Transactions.AddAsync(transaction);
        }

        public async Task<Transaction?> GetByIdForUserAsync(Guid transactionId, Guid userId)
        {
            return await _monioDbContext.Transactions
                .FirstOrDefaultAsync(q => q.Id == transactionId && q.UserId == userId);
        }

        public async Task<List<Transaction>> GetForUserAsync(
            Guid userId, 
            Guid? walletId,
            Guid? categoryId,
            TransactionType? type,
            DateOnly? fromDate,
            DateOnly? toDate,
            int page,
            int pageSize)
        {
            IQueryable<Transaction> query = _monioDbContext.Transactions
                .AsNoTracking()
                .Where(t => t.UserId == userId);

            if (walletId.HasValue)
            {
                query = query.Where(t => t.WalletId == walletId);
            }

            if (categoryId.HasValue) 
            {
                query = query.Where(t => t.CategoryId == categoryId.Value);
            }

            if (type.HasValue)
            {
                query = query.Where(t => t.Type == type.Value);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(t => t.TransactionDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(t => t.TransactionDate <= toDate.Value);
            }

            return await query
                .OrderByDescending(t => t.TransactionDate)
                .ThenByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
