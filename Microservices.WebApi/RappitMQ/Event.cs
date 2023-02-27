using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ
{

    public abstract class Event
    {
        public DateTimeOffset TimeStamp { get; private set; }
        public int Retry { get; private set; } = 0;
        public int RetryCount { get; protected set; } = 3;
        protected Event()
        {
            TimeStamp = DateTime.UtcNow;
        }

        public void SetRetruCount(short count)
        {
            RetryCount = count;
        }

        public void TryRetry()
        {
            if (IsTimeout()) throw new Exception("Retry equal or more than retry count");
            Retry++;
        }

        public bool IsTimeout()
        {
            return Retry >= RetryCount;
        }
    }
}

