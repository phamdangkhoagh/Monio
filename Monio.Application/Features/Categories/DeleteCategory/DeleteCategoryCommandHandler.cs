using MediatR;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Categories.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICacheService _cacheService;

        public DeleteCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            ICurrentUserService currentUserService,
            ICacheService cacheService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
            _cacheService = cacheService;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository
                 .GetByIdAndUserIdAsync(
                     request.Id,
                     _currentUserService.UserId);

            if (category == null) 
            {
                throw new Exception("Category not found.");
            }

            await _categoryRepository.DeleteAsync(category);

            await _cacheService.RemoveAsync($"categories:{category.UserId}");
        }
    }
}
