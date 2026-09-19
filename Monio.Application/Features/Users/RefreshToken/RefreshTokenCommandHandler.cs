using MediatR;
using Monio.Application.Features.Users.Login;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using RefreshTokenEntity = Monio.Domain.Entities.RefreshToken;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Users.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RefreshTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(
                request.RefreshToken,
                cancellationToken);

            if (refreshToken == null)
            {
                throw new Exception("Invalid refresh token!");
            }

            if (refreshToken.ExpiresAt <= DateTime.UtcNow)
            {
                throw new Exception("Refresh token has expired!");
            }

            if (refreshToken.RevokedAt != null)
            {
                throw new Exception("Refresh token has been revoked");
            }

            await _refreshTokenRepository.RevokeAsync(
                refreshToken,
                cancellationToken);

            var newRefreshToken = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                UserId = refreshToken.UserId,
                Token = GenerateRefreshToken(),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepository.AddAsync(
                newRefreshToken,
                cancellationToken);

            var accessToken = _jwtTokenGenerator.GenerateToken(refreshToken.User);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token
            };
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));
        }
    }
}
