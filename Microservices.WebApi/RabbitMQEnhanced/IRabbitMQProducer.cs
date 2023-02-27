using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQEnhanced
{
    internal interface IRabbitMQProducer
    {
        public void SendMessage<T>(T message);

    }

}
