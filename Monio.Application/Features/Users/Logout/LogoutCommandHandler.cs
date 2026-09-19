using MediatR;
using Monio.Application.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Users.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LogoutCommandHandler(
            IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(
                request.RefreshToken,
                cancellationToken);

            if (refreshToken == null)
            {
                throw new Exception("Invalid refresh token!");
            }

            if (refreshToken.RevokedAt != null)
            {
                return;
            }

            await _refreshTokenRepository.RevokeAsync(
                refreshToken,
                cancellationToken);
        }
    }
}
