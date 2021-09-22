using Microsoft.Extensions.Configuration;
using System.IO;

namespace MontyHallProblemSimulation.Infrastructure.Cqrs.Repository.Sqlite
{
    public class SqliteDbConnectionSettingsProvider : IDbConnectionSettingsProvider
    {
        private readonly IConfiguration configuration;

        public SqliteDbConnectionSettingsProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private void CreateDirectoryForSqliteDb(string dbConnectionString)
        {
            if (!string.IsNullOrWhiteSpace(dbConnectionString))
            {
                var directoryName = Path.GetDirectoryName(dbConnectionString);
                Directory.CreateDirectory(directoryName);
            }
        }

        private string ExtractFilePathFromConnectionString(string dbConnectionString)
        {
            string filePath = dbConnectionString.Replace("Data Source=", string.Empty);
            return filePath;
        }

        public string GetDbConnectionString(string connectionString)
        {
            var dbConnectionString = this.configuration.GetSection(connectionString)?.Value;
            if (!string.IsNullOrWhiteSpace(dbConnectionString))
            {
                var filePath = this.ExtractFilePathFromConnectionString(dbConnectionString);

                this.CreateDirectoryForSqliteDb(filePath);
            }
            return dbConnectionString;
        }
    }
}
