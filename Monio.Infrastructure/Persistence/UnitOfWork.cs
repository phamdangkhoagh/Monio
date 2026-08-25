using Monio.Application.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MonioDbContext _monioDbContext;

        public UnitOfWork(MonioDbContext monioDbContext)
        {
            _monioDbContext = monioDbContext;
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await _monioDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
