using cangulo.DbMigrationsHandler.Providers;
using FluentAssertions;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;

namespace cangulo.DbMigrationsHandler.UT.Providers
{
    public class EmbeddedScriptsProviderTest
    {
        [Fact]
        public void GetScripts_FilterByClientName()
        {
            // Arrange
            var expectedResult = new string[] { "0.0.0.01", "0.0.0.01-carlos", "0.0.0.02", "0.0.0.03", "0.0.0.03-carlos", "0.0.0.04" };

            var clientName = "carlos";
            var assemblyWithScripts = Assembly.GetAssembly(typeof(EmbeddedScriptsProviderTest));
            string CleanScriptName(string fullName)
            {
                // Extract the script name as 0.0.0.01 or 0.0.0.01-clientNAme

                var regex = new Regex($@"([\d]{{1,2}}\.){{3}}([\d]{{1,2}})(-{clientName})?");
                Match matchResult = regex.Match(fullName);
                if (matchResult.Success && fullName.Contains(".sql"))
                {
                    return matchResult.Groups[0].Value;
                }
                return fullName;
            }
            bool FilterScripts(string s) => !s.Contains("-") || s.ToLowerInvariant().Contains(clientName);

            var sut = new EmbeddedScriptsProvider(assemblyWithScripts, FilterScripts, CleanScriptName);

            // Act
            var result = sut.GetScripts(null);

            // Assert
            result.Select(x => x.Name).Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetScripts_FilterByBaseScripts()
        {
            // Arrange
            var expectedResult = new string[] { "0.0.0.01", "0.0.0.02", "0.0.0.03", "0.0.0.04" };

            var assemblyWithScripts = Assembly.GetAssembly(typeof(EmbeddedScriptsProviderTest));
            string CleanScriptName(string fullName)
            {
                // Extract the script name as 0.0.0.01 or 0.0.0.01-clientNAme

                var regex = new Regex($@"([\d]{{1,2}}\.){{3}}([\d]{{1,2}})");
                Match matchResult = regex.Match(fullName);
                return matchResult.Success && fullName.Contains(".sql") ? matchResult.Groups[0].Value : fullName;
            }
            bool FilterScripts(string s) => !s.Contains("-");

            var sut = new EmbeddedScriptsProvider(assemblyWithScripts, FilterScripts, CleanScriptName);

            // Act
            var result = sut.GetScripts(null);

            // Assert
            result.Select(x => x.Name).Should().BeEquivalentTo(expectedResult);
        }
    }
}