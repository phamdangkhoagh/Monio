using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Interfaces.Persistence
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetForUserAsync(Guid userId);
        Task<Category?> GetByIdAndUserIdAsync(Guid id, Guid userId);
        Task<Category?> GetForTransactionAsync(Guid categoryId, Guid userId);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
