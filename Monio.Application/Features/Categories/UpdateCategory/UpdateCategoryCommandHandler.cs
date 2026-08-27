using MediatR;
using Monio.Application.Features.Categories.GetCategories;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Categories.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICacheService _cacheService;

        public UpdateCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            ICurrentUserService currentUserService,
            ICacheService cacheService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
            _cacheService = cacheService;
        }

        public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var category = await _categoryRepository
                .GetByIdAndUserIdAsync(request.Id, userId);

            if (category == null) 
            {
               throw new Exception("Category not found.");
            }

            category.Name = request.Name;
            category.Icon = request.Icon;
            category.Color = request.Color;
            category.Type = request.Type;
            category.ParentId = request.ParentId;

            await _categoryRepository.UpdateAsync(category);

            await _cacheService.RemoveAsync($"categories:{category.UserId}");

            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Icon = category.Icon,
                Color = category.Color,
                Type = category.Type,
                IsSystem = category.IsSystem,
                ParentId = category.ParentId,
            };
        }
    }
}