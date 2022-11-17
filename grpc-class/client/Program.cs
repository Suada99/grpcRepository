

using Greet;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace client
{
    public class Program
    {
        const string target = "127.0.0.1:50051";

        static async Task Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);
            await channel.ConnectAsync().ContinueWith((task) =>
             {
                 if (task.Status == TaskStatus.RanToCompletion)
                     Console.WriteLine("The client connected successfully");
             });
            // var client = new DummyService.DummyServiceClient(channel);
            var client = new GreetingService.GreetingServiceClient(channel);
            var greeting = new Greeting
            {
                FirstName = "Ada",
                LastNsme = "Kumrija"
            };
            //var request = new GreetingRequest() { Greeting = greeting };
            //var response = client.Greet(request);
            //var client = new CalculatorService.CalculatorServiceClient(channel);
            //var request = new CalculateRequest
            //{
            //    FirstNum = 3,
            //    SecondNum = 4
            //};
            //var response = client.Calculate(request);
            var request = new GreetManyTimesRequest() { Greeting = greeting };
            var response = client.GreetManyTimes(request);

            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                await Task.Delay(200);
            }
            // Console.WriteLine(response..Result);
            channel.ShutdownAsync().Wait();
            Console.ReadKey();

        }
    }
}
