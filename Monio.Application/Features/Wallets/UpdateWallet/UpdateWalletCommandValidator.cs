using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Wallets.UpdateWallet
{
    public class UpdateWalletCommandValidator : AbstractValidator<UpdateWalletCommand>
    {
        public UpdateWalletCommandValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty()
                .WithMessage("Wallet ID không được để trống.");

            RuleFor(q => q.Name)
                .NotEmpty()
                .WithMessage("Tên ví không được để trống.")
                .MaximumLength(100)
                .WithMessage("Tên ví không được vượt quá 100 ký tự.");

            RuleFor(q => q.Type)
                .IsInEnum()
                .WithMessage("Loại ví không hợp lệ.");

            RuleFor(q => q.Currency)
                .NotEmpty()
                .WithMessage("Currency không được để trống.")
                .Length(3)
                .WithMessage("Currency phải có 3 ký tự.");
        }
    }
}
