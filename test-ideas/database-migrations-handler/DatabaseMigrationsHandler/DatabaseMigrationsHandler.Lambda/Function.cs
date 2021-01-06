using System.Collections.Generic;
using System.Net;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json.Serialization;
using DatabaseMigrationsHandler.Lambda.Models;
using cangulo.DbMigrationsHandler.Models;
using System.Text.RegularExpressions;
using System.Reflection;
using cangulo.DbMigrationsHandler.Scripts;
using cangulo.DbMigrationsHandler;
using DatabaseMigrationsHandler.Lambda.Logger;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DatabaseMigrationsHandler.Lambda
{
    public class Functions
    {
        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions()
        {
        }


        /// <summary>
        /// A Lambda function to respond to HTTP Post methods from API Gateway
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The API Gateway response.</returns>
        public APIGatewayProxyResponse Post(MigrationRequest request, ILambdaContext context)
        {
            context.Logger.LogLine("Get Request\n");

            var migrationSettings = new MigrationSettings
            {
                ConnectionString = request.ConnectionString,
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

            var dbUpdateHandler = new DbUpdatesHandler(migrationSettings, new LambdaLoggerWrapper<DbUpdatesHandler>(context));

            var result = dbUpdateHandler.UpdateDb();

            if (result.IsCompletedSuccessfully)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = "Hello AWS Serverless",
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };
            }
            else
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Body = "Error updating the DB",
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };
            }
        }
    }
}
