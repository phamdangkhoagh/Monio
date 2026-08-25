using MediatR;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Categories.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetCategoriesQueryHandler(
            ICategoryRepository categoryRepository,
            ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var categories = await _categoryRepository
                .GetForUserAsync(userId);

            return categories.Select(category => new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Icon = category.Icon,
                Color = category.Color,
                Type = category.Type,
                IsSystem = category.IsSystem,
                ParentId = category.ParentId
            }).ToList();
        }
    }
}
