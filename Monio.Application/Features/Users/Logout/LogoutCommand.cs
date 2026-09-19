using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Users.Logout
{
    public  class LogoutCommand : IRequest
    {
        public string RefreshToken { get; set; } = default!;
    }
}
