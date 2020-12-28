using DbUp.Engine;
using DbUp.Engine.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace cangulo.DbMigrationsHandler.Providers
{
    public class EmbeddedScriptsProvider : IScriptProvider
    {
        private readonly Assembly _scriptsAssembly;
        private readonly Func<string, bool> _filterSripts;
        private readonly Func<string, string> _renameScripts;

        public EmbeddedScriptsProvider(Assembly scriptsAssembly)
        {
            _scriptsAssembly = scriptsAssembly ?? throw new ArgumentNullException(nameof(scriptsAssembly));
            _filterSripts = (x) => true;
            _renameScripts = (x) => x;
        }

        public EmbeddedScriptsProvider(Assembly scriptsAssembly, Func<string, bool> filterScripts, Func<string, string> renameScripts)
        {
            _scriptsAssembly = scriptsAssembly ?? throw new ArgumentNullException(nameof(scriptsAssembly));
            _filterSripts = filterScripts ?? throw new ArgumentNullException(nameof(filterScripts));
            _renameScripts = renameScripts ?? throw new ArgumentNullException(nameof(renameScripts));
        }

        public IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager)
            => _scriptsAssembly
                .GetManifestResourceNames()
                .Where(_filterSripts)
                .Select(x =>
                {
                    var scriptName = _renameScripts == null ? x : _renameScripts(x);
                    return SqlScript.FromStream(scriptName, _scriptsAssembly.GetManifestResourceStream(x));
                })
                .OrderBy(x => x.Name);
    }
}