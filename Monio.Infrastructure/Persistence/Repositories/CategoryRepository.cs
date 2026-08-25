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

        public async Task AddAsync(Category category)
        {
            await _monioDbContext.Categories.AddAsync(category);
            await _monioDbContext.SaveChangesAsync();
        }

        public async Task<Category?> GetByIdAndUserIdAsync(Guid id, Guid userId)
        {
            return await _monioDbContext.Categories
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.UserId == userId &&
                    !c.IsSystem);
        }

        public async Task<List<Category>> GetForUserAsync(Guid userId)
        {
            return await _monioDbContext.Categories
                .Where(c => c.IsSystem || c.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _monioDbContext.Categories.Update(category);
            await _monioDbContext.SaveChangesAsync();
        }
    }
}
