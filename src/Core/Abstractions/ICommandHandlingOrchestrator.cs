namespace MontyHallProblemSimulation.Infrastructure.Core.Abstractions
{
    public interface ICommandHandlingOrchestrator
    {
        delegate void CommandReceivedDelegate<in TCommand>(TCommand command)
            where TCommand : Command;

        event CommandReceivedDelegate<Command> CommandReceivedEvent;

        void InitiateCommandHandling<TCommand>(TCommand command)
            where TCommand : Command;

        
    }
}
