using Microsoft.EntityFrameworkCore;
using Monio.Application.Interfaces.Persistence;
using Monio.Domain.Entities;


namespace Monio.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MonioDbContext _monioDbContext;

        public UserRepository(MonioDbContext monioDbContext)
        {
            _monioDbContext = monioDbContext;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await _monioDbContext.Users.AddAsync(user, cancellationToken);
            await _monioDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _monioDbContext.Users.AnyAsync(
                q => q.Email == email,
                cancellationToken
            );
        }
    }
}
