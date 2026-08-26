using Microsoft.Extensions.DependencyInjection;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using Monio.Infrastructure.Persistence;
using Monio.Infrastructure.Persistence.Dapper;
using Monio.Infrastructure.Persistence.Repositories;
using Monio.Infrastructure.Services;

namespace Monio.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DapperConnectionFactory>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();

            return services;
        }
    }
}
