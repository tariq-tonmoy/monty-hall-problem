namespace MontyHallProblemSimulation.Infrastructure.Cqrs.Repository
{
    public interface IDbConnectionSettingsProvider
    {
        string GetDbConnectionString(string dbConnectionKey);
    }
}
