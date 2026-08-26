using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monio.Application.Features.Dashboard.GetDashboardSummary;
using Monio.Application.Features.Dashboard.GetExpenseByCategory;

namespace Monio.API.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary(
            [FromQuery] GetDashboardSummaryQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("expense-by-category")]
        public async Task<IActionResult> GetExpenseByCategory(
            [FromQuery] GetExpenseByCategoryQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
