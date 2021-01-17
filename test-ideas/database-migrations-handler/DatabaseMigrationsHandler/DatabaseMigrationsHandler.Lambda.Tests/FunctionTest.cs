using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using DatabaseMigrationsHandler.Lambda.Models;
using Xunit;

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