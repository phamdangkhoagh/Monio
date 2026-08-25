using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monio.Application.Features.Categories.GetCategories;

namespace Monio.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCategoriesQuery(), cancellationToken);

            return Ok(result);
        }
    }
}
