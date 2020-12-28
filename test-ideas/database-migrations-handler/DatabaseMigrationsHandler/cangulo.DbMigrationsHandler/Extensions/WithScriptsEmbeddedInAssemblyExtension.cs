using cangulo.DbMigrationsHandler.Providers;
using DbUp.Builder;
using System;
using System.Reflection;

namespace cangulo.DbMigrationsHandler.Extensions
{
    public static class WithScriptsEmbeddedInAssemblyExtension
    {
        public static UpgradeEngineBuilder WithScriptsEmbeddedInAssembly(
                                                this UpgradeEngineBuilder builder,
                                                Assembly assembly,
                                                Func<string, bool> filterScripts,
                                                Func<string, string> renameScripts)
        {
            builder.Configure(c => c.ScriptProviders.Add(new EmbeddedScriptsProvider(assembly, filterScripts, renameScripts)));
            return builder;
        }
    }
}