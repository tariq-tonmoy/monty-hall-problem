using MontyHallProblemSimulation.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Infrastructure.Cqrs.Repository
{
    public interface IAggregateRootRepository<T>
        where T : AggregateRoot
    {
        Task SaveAsync(T aggregateRoot);

        Task UpdateAsync(T aggregateRoot);

        Task<T> GetByIdAsync(Guid id);

        Task<bool> ExistsAsync(Guid id);
    }
}
