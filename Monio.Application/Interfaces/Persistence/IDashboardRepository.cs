using Monio.Application.Features.Dashboard.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Interfaces.Persistence
{
    public interface IDashboardRepository
    {
        Task<DashboardSummaryResponse> GetSummaryAsync(
            Guid userId,
            DateOnly? fromDate,
            DateOnly? toDate);
    }
}
