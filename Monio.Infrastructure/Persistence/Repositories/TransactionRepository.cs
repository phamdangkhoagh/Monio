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
    }
}
