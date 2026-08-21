using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Users.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(q => q.Email)
                .NotEmpty()
                .WithMessage("Email không được để trống.")
                .EmailAddress()
                .WithMessage("Email không đúng định dạng.")
                .MaximumLength(255)
                .WithMessage("Email không được vượt quá 100 ký tự.");

            RuleFor(q => q.Password)
                .NotEmpty()
                .WithMessage("Mật khẩu không được để trống.")
                .MinimumLength(8)
                .WithMessage("Mật khẩu phải có ít nhất 8 ký tự.");

            RuleFor(q => q.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Vui lòng nhập lại mật khẩu.")
                .Equal(q => q.Password)
                .WithMessage("Mật khẩu xác nhận không khớp.");

            RuleFor(q => q.FullName)
                .NotEmpty()
                .WithMessage("Họ tên không được để trống.")
                .MaximumLength(100)
                .WithMessage("Họ tên không được vượt quá 100 ký tự.");
        }
    }
}
