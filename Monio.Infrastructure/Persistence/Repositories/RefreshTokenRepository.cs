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
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly MonioDbContext _monioDbContext;

        public RefreshTokenRepository(MonioDbContext monioDbContext)
        {
            _monioDbContext = monioDbContext;
        }

        public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            await _monioDbContext.RefreshTokens.AddAsync(
                refreshToken,
                cancellationToken);

            await _monioDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await _monioDbContext.RefreshTokens
                 .Include(q => q.User)
                 .FirstOrDefaultAsync(
                   q => q.Token == token,
                   cancellationToken);
        }

        public async Task RevokeAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            refreshToken.RevokedAt = DateTime.UtcNow;

            await _monioDbContext.SaveChangesAsync (cancellationToken);
        }
    }
}
