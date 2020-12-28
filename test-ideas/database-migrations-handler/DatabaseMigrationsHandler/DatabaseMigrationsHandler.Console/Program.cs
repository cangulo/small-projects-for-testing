using cangulo.DbMigrationsHandler;
using cangulo.DbMigrationsHandler.Models;
using cangulo.DbMigrationsHandler.Scripts;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DatabaseMigrationsHandler.Console
{
    [Command(Name = "DatabaseMigrationsHandler.Console.exe", Description = "\nThis app will help you deploy updates")]
    [HelpOption("-?")]
    public class Program
    {
        [Option("-c|--connectionString", Description = "Please provide the connection string for the DB", ShowInHelpText = true)]
        [Required]
        public string ConnectionString { get; }

        public static Task<int> Main(string[] args) => CommandLineApplication.ExecuteAsync<Program>(args);
        private async Task OnExecute()
        {
            var services = new ServiceCollection();

            // TODO: Get this parameters from CLI arguments
            var appSettings = new MigrationSettings
            {
                ConnectionString = ConnectionString,
                ScriptsAssembly = Assembly.GetAssembly(typeof(DbScriptsConfig)),
                FilterScripts = (s) => s.Contains(".sql") && !s.ToLowerInvariant().Contains("clientspecifics"),
                RenameScripts = (s) =>
                {
                    // Extract the script name as 0.0.0.01
                    var regex = new Regex($@"([\d]{{1,2}}\.){{3}}([\d]{{1,2}})");
                    var matchResult = regex.Match(s);
                    return matchResult.Success && s.Contains(".sql") ? matchResult.Groups[0].Value : s;
                }
            };

            ConfigureServices(services, appSettings);

            var serviceProvider = services.BuildServiceProvider();
            var updater = serviceProvider.GetService<IDbUpdatesHandler>();
            await updater.UpdateDb();
        }
        private static void ConfigureServices(ServiceCollection services, MigrationSettings appSettings)
        {
            services
                .AddLogging(build =>
                    {
                        build.AddConsole();
                        build.AddDebug();
                    })
                .AddSingleton<MigrationSettings>(appSettings)
                .AddSingleton<IDbUpdatesHandler, DbUpdatesHandler>();
        }
    }
}