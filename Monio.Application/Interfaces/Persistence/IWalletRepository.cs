using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Interfaces.Persistence
{
    public interface IWalletRepository
    {
        Task<List<Wallet>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Wallet wallet);
        Task<Wallet?> GetByIdAndUserIdAsync(Guid walletId, Guid userId);
        Task UpdateAsync(Wallet wallet);
        Task DeleteAsync(Wallet wallet);
    }
}
