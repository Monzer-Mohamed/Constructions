using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{


    public sealed class RabbitMQHandler : IEventBus
    {
        private readonly IConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly ILogger<RabbitMQHandler> _logger;
        private readonly List<Type> _eventType;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQHandler(
            IConnectionFactory factory,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<RabbitMQHandler> logger
            )
        {
            _logger = logger;
            _factory = factory;
            _serviceScopeFactory = serviceScopeFactory;
            _eventType = new List<Type>();
            _handlers = new Dictionary<string, List<Type>>();
            try
            {
                _connection = _factory.CreateConnection();
            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }
            finally
            {

            }
        }

        public Task Publish<T>(T @event, System.Threading.CancellationToken cancellationToken = default) where T : Event
        {
            if (cancellationToken != default && cancellationToken.IsCancellationRequested)
                return Task.FromCanceled(cancellationToken);

            if (_connection != null)
                using (var channel = _connection.CreateModel())
                {
                    string evenName = @event.GetType().Name;
                    //System.Console.WriteLine(evenName);
                    channel.QueueDeclare(evenName, true, false, false, null);
                    channel.BasicQos(0, 1, false);

                    string message = JsonConvert.SerializeObject(@event);
                    byte[] body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish("", evenName, properties, body);
                }
            return Task.CompletedTask;
        }

        public Task Subscribe<T>(Func<T, Task>  action)
            where T : Event
        {
            if (_connection != null)
            {
                string evenName = typeof(T).Name;

                if (!_eventType.Contains(typeof(T)))
                    _eventType.Add(typeof(T));

                if (!_handlers.ContainsKey(evenName))
                    _handlers.Add(evenName, new List<Type>());


                StartBasicConsume<T>(action);
            }
            return Task.CompletedTask;
        }

        private void StartBasicConsume<T>(Func<T,Task> action)
        where T : Event
        {
            IModel channel = _connection.CreateModel();
            string evenName = typeof(T).Name;

            channel.QueueDeclare(evenName, true, false, false, null);
            channel.BasicQos(0, 1, false);
            // channel.WaitForConfirms()

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += (sender, @event) =>
                Consumer_Received<T>(sender, @event, channel, action);

            channel.BasicConsume(consumer, evenName, false);
        }

        private async Task Consumer_Received<T>(object sender, BasicDeliverEventArgs @event, IModel channel, Func<T, Task> action)
        where T : Event
        {
            var evenName = @event.RoutingKey;
            var message = Encoding.UTF8.GetString(@event.Body.ToArray());
            try
            {
                await ProcessEvent<T>(evenName, message, action).ConfigureAwait(false);
                channel.BasicAck(deliveryTag: @event.DeliveryTag, multiple: false);
            }
            catch (Exception e)
            {
                channel.BasicReject(@event.DeliveryTag, true);
                _logger.LogError(e, $"Fail rabbitMQ handler: {@event.DeliveryTag}");
            }
            await Task.Yield();
        }
        private async Task ProcessEvent<T>(string evenName, string message, Func<T, Task> action)
                where T : Event
        {
            if (_handlers.ContainsKey(evenName))
            {
                    var @event = JsonConvert.DeserializeObject<T>(message);
                    await action(@event);
                }
            await Task.Yield();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
