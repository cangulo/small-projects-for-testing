using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

using DatabaseMigrationsHandler.Lambda;
using DatabaseMigrationsHandler.Lambda.Models;

namespace DatabaseMigrationsHandler.Lambda.Tests
{
    public class FunctionTest
    {
        public FunctionTest()
        {
        }

        [Fact]
        public void TetGetMethod()
        {
            TestLambdaContext context;
            MigrationRequest request;
            APIGatewayProxyResponse response;

            Functions functions = new Functions();


            request = new MigrationRequest();
            context = new TestLambdaContext();
            response = functions.Post(request, context);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Hello AWS Serverless", response.Body);
        }
    }
}
