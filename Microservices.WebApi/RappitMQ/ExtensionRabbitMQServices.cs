using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ
{
    public static class ExtensionRabbitMQServices
    {
        public static void AddRabbitMQ(this IServiceCollection service,
            string ClientName, Func<CofigurationSettingModel> connection)
        {
            var config = connection();
            service.AddSingleton<IConnectionFactory>(o => new ConnectionFactory
            {
                //"Hostname": "92.205.18.114",
                //"Port": "5672"
                HostName = config.Hostname,
                // UserName = "Guest",// config.Username,
                //Password = "Guest",//config.Password,
                Port = config.Port,
                // VirtualHost = config.VirtualHost,
                AutomaticRecoveryEnabled = true,
                DispatchConsumersAsync = true,
                ClientProvidedName = ClientName,
            });

            service.AddSingleton<IEventBus, RabbitMQHandler>();
        }
    }
}
