using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Interfaces.Services
{
    public class AccessTokenResult
    {
        public string Token { get; set; } = default!;
        public int ExpiresIn { get; set; }
    }
}
