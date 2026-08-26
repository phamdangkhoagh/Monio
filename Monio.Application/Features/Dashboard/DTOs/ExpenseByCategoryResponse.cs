using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Dashboard.DTOs
{
    public class ExpenseByCategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
        public decimal TotalAmount { get; set; }
    }
}
