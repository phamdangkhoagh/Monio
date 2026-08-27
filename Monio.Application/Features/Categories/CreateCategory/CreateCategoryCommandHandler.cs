using MediatR;
using Monio.Application.Features.Categories.GetCategories;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Categories.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICacheService _cacheService;

        public CreateCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            ICurrentUserService currentUserService,
            ICacheService cacheService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
            _cacheService = cacheService;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                UserId = _currentUserService.UserId,
                Name = request.Name,
                Icon = request.Icon,
                Color = request.Color,
                Type = request.Type,
                IsSystem = false,
                ParentId = request.ParentId
            };

            await _categoryRepository.AddAsync(category);

            await _cacheService.RemoveAsync($"categories:{category.UserId}");

            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Icon = category.Icon,
                Type = category.Type,
                IsSystem = category.IsSystem,
                ParentId = category.ParentId
            };
        }
    }
}
