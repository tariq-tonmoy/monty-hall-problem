using Microsoft.EntityFrameworkCore;
using MontyHallProblemSimulation.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Infrastructure.Cqrs.Repository.Sqlite
{
    internal class SqliteReadRepository<T, TDbContext> : IReadRepository<T>
        where T : ViewModelBase
        where TDbContext : DbContext
    {
        private readonly TDbContext dbContext;

        public SqliteReadRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbContext.Database.Migrate();
        }

        public IQueryable<T> GetByFilter(Expression<Func<T, bool>> dataFilters)
        {
            return dbContext.Set<T>()?.Where(dataFilters);
        }

        public async Task SaveAsync(T viewModel)
        {
            var entityEntry = dbContext.Set<T>().Add(viewModel);
            await dbContext.SaveChangesAsync();
            entityEntry.State = EntityState.Detached;
        }

        public async Task UpdateAsync(T viewModel)
        {
            var entityEntry = dbContext.Set<T>().Update(viewModel);
            await dbContext.SaveChangesAsync();
            entityEntry.State = EntityState.Detached;
        }

        public async Task DeleteAsync(T viewModel)
        {
            var entityEntry = dbContext.Set<T>().Remove(viewModel);
            await dbContext.SaveChangesAsync();
            entityEntry.State = EntityState.Detached;
        }
    }
}
