using MediatR;
using Monio.Application.Features.Dashboard.DTOs;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Dashboard.GetDashboardSummary
{
    public class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQuery, DashboardSummaryResponse>
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetDashboardSummaryQueryHandler(
            IDashboardRepository dashboardRepository,
            ICurrentUserService currentUserService)
        {
            _dashboardRepository = dashboardRepository;
            _currentUserService = currentUserService;
        }


        public async Task<DashboardSummaryResponse> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            return await _dashboardRepository.GetSummaryAsync(
                userId,
                request.FromDate,
                request.ToDate);
        }
    }
}
