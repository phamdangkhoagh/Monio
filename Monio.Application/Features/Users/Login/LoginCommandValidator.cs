using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Users.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(q => q.Email)
                .NotEmpty()
                .WithMessage("Email không được để trống.")
                .EmailAddress()
                .WithMessage("Email không đúng định dạng.")
                .MaximumLength(255)
                .WithMessage("Email không được vượt quá 255 ký tự.");

            RuleFor(q => q.Password)
                .NotEmpty()
                .WithMessage("Mật khẩu không được để trống.")
                .MinimumLength(8)
                .WithMessage("Mật khẩu phải có ít nhất 8 ký tự.");
        }
    }
}
