using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSTestSQS.Services
{
    public interface IEnqueueMessageSQS
    {
        public Task EnqueueAsync();

        public Task<IEnumerable<string>> GetQueues();
    }

    public class EnqueueMessageSQS : IEnqueueMessageSQS
    {
        private readonly IAmazonSQS _sqsClient;

        public EnqueueMessageSQS(IAmazonSQS sqsClient)
        {
            _sqsClient = sqsClient ?? throw new ArgumentNullException(nameof(sqsClient));
        }

        public async Task EnqueueAsync()
        {
            //var credentials = new AwsCredentials
            //{
            //    aws_access_key_id = "",
            //    aws_secret_access_key = ""
            //};

            var responseList = await _sqsClient.ListQueuesAsync("");
        }

        public async Task<IEnumerable<string>> GetQueues()
        {

            var responseList1 = await _sqsClient.ListQueuesAsync("");
            return responseList1.QueueUrls;

            //var chain = new CredentialProfileStoreChain();
            //AWSCredentials awsCredentials;
            //if (chain.TryGetAWSCredentials("default", out awsCredentials))
            //{
            //    // Use awsCredentials to create an Amazon S3 service client
            //    using (var client = new AmazonSQSClient(awsCredentials))
            //    {
            //        var responseList = client.ListQueuesAsync("");
            //        return responseList.QueueUrls;
            //    }
            //}
            //return new List<string> { "hola" };
        }
    }
}