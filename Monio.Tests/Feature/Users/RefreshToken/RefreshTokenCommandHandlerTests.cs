using Monio.Application.Features.Users.RefreshToken;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using Monio.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RefreshTokenEntity = Monio.Domain.Entities.RefreshToken;

namespace Monio.Tests.Feature.Users.RefreshToken
{
    [TestFixture]
    public class RefreshTokenCommandHandlerTests
    {
        [Test]
        public async Task Handle_ShouldThrow_WhenRefreshTokenDoesNotExist()
        {
            // Arrange
            var repositoryMock = new Mock<IRefreshTokenRepository>();

            repositoryMock
                .Setup(q => q.GetByTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((RefreshTokenEntity?)null);

            var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

            var command = new RefreshTokenCommand
            {
                RefreshToken = "invalid-token"
            };

            
            //Act & Assert
            var handler = new RefreshTokenCommandHandler(
                repositoryMock.Object,
                jwtTokenGeneratorMock.Object);

            var exception = Assert.ThrowsAsync<Exception>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);

            });

            Assert.That(exception!.Message, Is.EqualTo("Invalid refresh token!"));
        }

        [Test]
        public async Task Handle_ShouldThrow_WhenRefreshTokenIsExpired()
        {
            // Arrange
            var repositoryMock = new Mock<IRefreshTokenRepository>();

            var refreshToken = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Token = "expired-token",
                ExpiresAt = DateTime.UtcNow.AddMinutes(-1),
                CreatedAt = DateTime.UtcNow.AddDays(-1),
            };

            repositoryMock
                .Setup(q => q.GetByTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(refreshToken);

            var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

            var command = new RefreshTokenCommand
            {
                RefreshToken = "expired-token"
            };

            var handler = new RefreshTokenCommandHandler(
                repositoryMock.Object,
                jwtTokenGeneratorMock.Object);

            //Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);

            });

            Assert.That(exception!.Message,Is.EqualTo("Refresh token has expired!"));
        }

        [Test]
        public async Task Handle_ShouldThrow_WhenRefreshTokenIsRevoked()
        {
            // Arrange
            var repositoryMock = new Mock<IRefreshTokenRepository>();

            var refreshToken = new RefreshTokenEntity       
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Token = "expired-token",
                ExpiresAt = DateTime.UtcNow.AddMinutes(1),
                RevokedAt = DateTime.UtcNow.AddDays(-1),
                CreatedAt = DateTime.UtcNow.AddDays(-1),
            };

            repositoryMock
                .Setup(q => q.GetByTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(refreshToken);

            var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

            var command = new RefreshTokenCommand
            {
                RefreshToken = "revoked-token"
            };

            var handler = new RefreshTokenCommandHandler(repositoryMock.Object, jwtTokenGeneratorMock.Object);

            //Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);

            });

            Assert.That(exception!.Message, Is.EqualTo("Refresh token has been revoked"));  
        }

        [Test]
        public async Task Handle_ShouldRefreshToken_WhenRefreshTokenIsValid()
        {
            // Arrange
            var repositoryMock = new Mock<IRefreshTokenRepository>();

            var userId = Guid.NewGuid();

            var refreshToken = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = "old-refresh-token",
                ExpiresAt = DateTime.UtcNow.AddDays(1),
                CreatedAt = DateTime.UtcNow.AddDays(-1),    
                RevokedAt = null,

                User = new User
                {
                    Id = userId,
                    Email = "test@example.com"
                }
            };

            repositoryMock
                .Setup(q => q.GetByTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(refreshToken);

            repositoryMock
                .Setup(q => q.RevokeAsync(
                    refreshToken,
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            repositoryMock
                .Setup(q => q.AddAsync(
                    It.IsAny<RefreshTokenEntity>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

            jwtTokenGeneratorMock
                .Setup(q => q.GenerateToken(It.IsAny<User>()))
                .Returns(new AccessTokenResult
                {
                    Token = "new-access-token",
                    ExpiresIn = 3600
                });

            var command = new RefreshTokenCommand
            {
                RefreshToken = "old-refresh-token"
            };

            var handler = new RefreshTokenCommandHandler(repositoryMock.Object, jwtTokenGeneratorMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.AccessToken, Is.EqualTo("new-access-token"));
            Assert.That(result.ExpiresIn, Is.EqualTo(3600));
            Assert.That(result.RefreshToken, Is.Not.Null.And.Not.Empty);

            repositoryMock.Verify(
                q => q.RevokeAsync(
                    refreshToken,
                    It.IsAny<CancellationToken>()),
                Times.Once);

            repositoryMock.Verify(
                q => q.AddAsync(
                    It.IsAny<RefreshTokenEntity>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            jwtTokenGeneratorMock.Verify(
                q => q.GenerateToken(refreshToken.User),
                Times.Once);

        }

    }
}
