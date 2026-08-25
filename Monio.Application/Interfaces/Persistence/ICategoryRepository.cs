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
    }
}
