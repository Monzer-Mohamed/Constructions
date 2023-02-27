
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcDemo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Grpc.Service
{
    public class Client : BackgroundService
    {
        private readonly ILogger<Client> _logger;
        private readonly string _url;

        public Client(ILogger<Client> logger, IConfiguration configuration)
        {
            _logger = logger;
            _url = configuration["Kestrel:Endpoints:gRPC:Url"];
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(Object), Encoding.UTF8, "application/json");
            string endpoint = "https://localhost:44319/gateway/Api/Constructions/";
            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                    //    TempData["Profile"] = JsonConvert.SerializeObject(user);
                    //    return RedirectToAction("Profile");
                    }
                    
                }

            }
            //using var channel = GrpcChannel.ForAddress(_url);
            //var client = new Greeter.GreeterClient(channel);

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    var reply = await client.SayHelloAsync(new HelloRequest
            //    {
            //        Name = "Worker"
            //    });

            //    _logger.LogInformation("Greeting: {reply.Message} -- {DateTime.Now}");
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}


