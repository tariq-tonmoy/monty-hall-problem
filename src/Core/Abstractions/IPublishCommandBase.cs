using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Infrastructure.Core.Abstractions
{
    public interface IPublishCommandBase
    {
        Task PublishMessageAsync<TCommand>(TCommand command)
            where TCommand : Command;
    }
}
