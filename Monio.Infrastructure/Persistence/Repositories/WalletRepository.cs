using Microsoft.EntityFrameworkCore;
using Monio.Application.Interfaces.Persistence;
using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Infrastructure.Persistence.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly MonioDbContext _monioDbContext;

        public WalletRepository(MonioDbContext monioDbContext)
        {
            _monioDbContext = monioDbContext;
        }

        public async Task<List<Wallet>> GetByUserIdAsync(Guid userId)
        {
            return await _monioDbContext.Wallets
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }
    }
}
