using Monio.Application.Interfaces.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                if (!Guid.TryParse(userId, out var id))
                    throw new UnauthorizedAccessException("User is not authenticated.");

                return id;
            }
        }

        public string? Email =>
            _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue (ClaimTypes.Email);
    }
}
