using Microsoft.Extensions.DependencyInjection;
using Monio.Application.Interfaces;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
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

            return services;
        }
    }
}
