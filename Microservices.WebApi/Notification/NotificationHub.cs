using Microsoft.AspNetCore.SignalR;
using RabbitMQ;
using System;
using System.Threading.Tasks;

namespace Notification
{
    public class NotificationHub : Hub
    {
        private IEventBus _eventBus;
        public NotificationHub(IEventBus eventBus)
        {
            _eventBus = eventBus;
            //_eventBus.Publish(new NotificationMessage { })
        }

        //public async Task SendMessage(string user, string message)
        //{
         
        //    await Task.CompletedTask;
        //}
    }
}
