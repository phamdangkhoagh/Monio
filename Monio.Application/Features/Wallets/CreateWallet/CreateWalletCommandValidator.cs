using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Wallets.CreateWallet
{
    public class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
    {
        public CreateWalletCommandValidator() 
        {
            RuleFor(q => q.Name)
                .NotEmpty()
                .WithMessage("Tên ví không được để trống")
                .MaximumLength(100)
                .WithMessage("Tên ví không được vượt quá 100 kí tự");

            RuleFor(q => q.Currency)
                .NotEmpty()
                .Length(3)
                .WithMessage("Currency phải có 3 ký tự.");
        }
    }
}
