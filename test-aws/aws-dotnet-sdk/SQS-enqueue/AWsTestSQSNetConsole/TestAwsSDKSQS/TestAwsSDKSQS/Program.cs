using Amazon;
using Amazon.SQS;
using System;
using System.Threading.Tasks;

namespace TestAwsSDKSQS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("################################");
            Console.WriteLine("Amazon SQS!");
            Console.WriteLine("################################\n");

            IAmazonSQS sqs = new AmazonSQSClient(RegionEndpoint.EUWest3);

            var queuesList = await sqs.ListQueuesAsync("");
            queuesList.QueueUrls.ForEach(x => Console.WriteLine($"Queue:{x}"));
        }
    }
}
