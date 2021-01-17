using cangulo.DbMigrationsHandler.Extensions;
using cangulo.DbMigrationsHandler.Models;
using DbUp;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace cangulo.DbMigrationsHandler
{
    public class DbUpdatesHandler : IDbUpdatesHandler
    {
        private readonly MigrationSettings _migrationSettings;
        private readonly ILogger _logger;

        public DbUpdatesHandler(MigrationSettings migrationSettings, ILogger<DbUpdatesHandler> logger)
        {
            _migrationSettings = migrationSettings ?? throw new ArgumentNullException(nameof(migrationSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task UpdateDb()
        {
            _logger.LogInformation("STARTING MIGRATION");

            var connectionString = _migrationSettings.ConnectionString;

            EnsureDatabase.For.MySqlDatabase(connectionString);

            var upgradeEngine = DeployChanges
                                    .To
                                    .MySqlDatabase(connectionString)
                                    .WithScriptsEmbeddedInAssembly(
                                        _migrationSettings.ScriptsAssembly,
                                        _migrationSettings.FilterScripts,
                                        _migrationSettings.RenameScripts)
                                    .LogToConsole()
                                    .Build();

            if (!upgradeEngine.TryConnect(out string errorMsg))
            {
                _logger.LogError($"Error connecting to the DB:{errorMsg}");
                return Task.CompletedTask;
            }

            if (!upgradeEngine.IsUpgradeRequired())
            {
                _logger.LogInformation($"No script execution required");
                return Task.CompletedTask;
            }

            var result = upgradeEngine.PerformUpgrade();

            if (!result.Successful)
                _logger.LogError($"Error Executign the script {result.ErrorScript.Name}", result.Error);

            return Task.CompletedTask;
        }
    }
}