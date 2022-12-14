using Greet;
using Grpc.Core;
using System;
using System.IO;

namespace server
{
    public class Program
    {
        const int Port = 50051;
        static void Main(string[] args)
        {
            Server server = null;

            try
            {
                server = new Server()
                {
                    Services = { GreetingService.BindService(new GreetingServiceImpl()) },
                    //Services = { CalculatorService.BindService(new CalculatorServiceImpl()) },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                };
                server.Start();
                Console.WriteLine("The server is listening on the port :" + Port);
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine("Server failed to satrt :" + e.Message);
                throw;
            }
            finally
            {
                if (server != null)
                {
                    server.ShutdownAsync().Wait();
                }
            }
        }
    }
}
