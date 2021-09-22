namespace MontyHallProblemSimulation.Infrastructure.Core.Abstractions
{
    public interface IPublishEventService<in TPublishClient> : IPublishEventBase
        where TPublishClient : class
    {
    }
}
