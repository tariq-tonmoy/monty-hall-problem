namespace MontyHallProblemSimulation.Infrastructure.Core.Abstractions
{
    public interface IPublishCommandService<in TPublishClient> : IPublishCommandBase
        where TPublishClient : class
    {
    }
}
