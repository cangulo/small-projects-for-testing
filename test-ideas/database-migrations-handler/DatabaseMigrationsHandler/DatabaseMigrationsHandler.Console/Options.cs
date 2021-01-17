using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace DatabaseMigrationsHandler.Console
{
    public class Options
    {
        [Option("-c|--connectionString", Description = "Please provide the connection string for the DB", ShowInHelpText = true)]
        [Required]
        public string ConnectionString { get; }
    }
}