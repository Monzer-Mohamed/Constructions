using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public interface IEventBus : IDisposable
    {
        Task Publish<T>(T @event, CancellationToken cancellationToken = default) where T : Event;
        Task Subscribe<T>(Func<T, Task> action)
        where T : Event;
    }
    

}
