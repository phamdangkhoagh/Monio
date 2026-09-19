using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Interfaces.Persistence
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(
            RefreshToken refreshToken,
            CancellationToken cancellationToken);

        Task<RefreshToken?> GetByTokenAsync (
            string token,
            CancellationToken cancellationToken);

        Task RevokeAsync(
            RefreshToken refreshToken,
            CancellationToken cancellationToken);
    }
}
