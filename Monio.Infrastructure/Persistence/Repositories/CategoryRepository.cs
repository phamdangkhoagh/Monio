using Microsoft.EntityFrameworkCore;
using Monio.Application.Interfaces.Persistence;
using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MonioDbContext _monioDbContext;

        public CategoryRepository(MonioDbContext monioDbContext)
        {
            _monioDbContext = monioDbContext;
        }

        public async Task<List<Category>> GetForUserAsync(Guid userId)
        {
            return await _monioDbContext.Categories
                .Where(c => c.IsSystem || c.UserId == userId)
                .ToListAsync();
        }
    }
}
