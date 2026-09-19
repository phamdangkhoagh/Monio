using MediatR;
using Monio.Application.Features.Users.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Users.RefreshToken
{
    public class RefreshTokenCommand : IRequest<AuthResponse>
    {
        public string RefreshToken { get; set; } = default!;
    }
}
