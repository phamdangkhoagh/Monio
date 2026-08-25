using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Transactions.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(q => q.WalletId)
                .NotEmpty()
                .WithMessage("Wallet không được để trống.");

            RuleFor(q => q.CategoryId)
                .NotEmpty()
                .WithMessage("Category không được để trống.");

            RuleFor(q => q.Amount)
                .GreaterThan(0)
                .WithMessage("Số tiền phải lớn hơn 0.");

            RuleFor(q => q.Currency)
                .NotEmpty()
                .WithMessage("Currency không được để trống.")
                .Length(3)
                .WithMessage("Currency phải có 3 ký tự.");

            RuleFor(q => q.ExchangeRate)
                .GreaterThan(0)
                .WithMessage("Exchange rate phải lớn hơn 0.");

            RuleFor(q => q.Type)
                .IsInEnum()
                .WithMessage("Transaction type không hợp lệ.");

            RuleFor(q => q.TransactionDate)
                .NotEqual(default(DateOnly))
                .WithMessage("Ngày giao dịch không được để trống.");
        }
    }
}
