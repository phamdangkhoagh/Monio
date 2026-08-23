using MediatR;
using Monio.Application.Features.Users.Register;
using Monio.Application.Interfaces;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception("Invalid email or password!");
            }

            var isValidPassword = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

            if (!isValidPassword)
            {
                throw new Exception("Invalid email or password!");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponse
            {
                AccessToken = token
            };
        }
    }
}
