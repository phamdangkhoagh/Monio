using MediatR;
using Monio.Application.Features.Dashboard.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Dashboard.GetExpenseByCategory
{
    public class GetExpenseByCategoryQuery : IRequest<List<ExpenseByCategoryResponse>>
    {
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
    }
}
