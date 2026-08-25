using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Transactions.GetTransactions
{
    public class GetTransactionsQueryValidator : AbstractValidator<GetTransactionsQuery>
    {
        public GetTransactionsQueryValidator()
        {
            RuleFor(q => q.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page phải lớn hơn hoặc bằng 1.");

            RuleFor(q => q.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize phải từ 1 đến 100.");

            RuleFor(q => q.ToDate)
                .GreaterThanOrEqualTo(q => q.FromDate)
                .When(q => q.FromDate.HasValue && q.ToDate.HasValue)
                .WithMessage("ToDate không được nhỏ hơn FromDate.");
        }
    }
}
