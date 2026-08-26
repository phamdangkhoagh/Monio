using MediatR;
using Monio.Application.Features.Dashboard.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Dashboard.GetDashboardSummary
{
    public class GetDashboardSummaryQuery : IRequest<DashboardSummaryResponse>
    {
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
    }
}
