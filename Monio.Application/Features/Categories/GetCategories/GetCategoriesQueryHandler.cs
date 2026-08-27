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
        private readonly ICacheService _cacheService;

        public GetCategoriesQueryHandler(
            ICategoryRepository categoryRepository,
            ICurrentUserService currentUserService,
            ICacheService cacheService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
            _cacheService = cacheService;
        }

        public async Task<List<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var cacheKey = $"categories:{userId}";

            var cachedCategories = await _cacheService.GetAsync<List<CategoryResponse>>(cacheKey);

            if (cachedCategories != null) 
            {
                return cachedCategories;
            }

            var categories = await _categoryRepository
                .GetForUserAsync(userId);

            var result = categories.Select(category => new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Icon = category.Icon,
                Color = category.Color,
                Type = category.Type,
                IsSystem = category.IsSystem,
                ParentId = category.ParentId
            }).ToList();

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(10));

            return result;
        }
    }
}
