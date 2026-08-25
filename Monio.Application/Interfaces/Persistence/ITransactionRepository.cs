using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Infrastructure.Persistence.Repositories
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task<List<Transaction>> GetForUserAsync(
            Guid userId,
            Guid? walletId,
            Guid? categoryId,
            TransactionType? type,
            DateOnly? fromDate,
            DateOnly? toDate,
            int page,
            int pageSize);
    }
}
