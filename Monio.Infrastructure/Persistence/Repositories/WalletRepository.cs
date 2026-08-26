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

        public async Task AddAsync(Wallet wallet)
        {
            await _monioDbContext.Wallets.AddAsync(wallet);
            await _monioDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Wallet wallet)
        {
            _monioDbContext.Wallets.Remove(wallet);
            await _monioDbContext.SaveChangesAsync();
        }

        public async Task<Wallet?> GetByIdAndUserIdAsync(Guid walletId, Guid userId)
        {
            return await _monioDbContext.Wallets
                .FirstOrDefaultAsync(w =>
                    w.Id == walletId &&
                    w.UserId == userId);
        }

        public async Task<List<Wallet>> GetByUserIdAsync(Guid userId)
        {
            return await _monioDbContext.Wallets
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            _monioDbContext.Wallets.Update(wallet);
            await _monioDbContext.SaveChangesAsync();
        }
    }
}
