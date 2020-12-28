using System;
using System.Reflection;

namespace cangulo.DbMigrationsHandler.Models
{
    public class MigrationSettings
    {
        public string ConnectionString { get; set; }
        public Assembly ScriptsAssembly { get; set; }
        public Func<string, string> RenameScripts { get; set; }
        public Func<string, bool> FilterScripts { get; set; }
    }
}