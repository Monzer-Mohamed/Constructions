using RabbitMQ;
using System;

namespace Notification
{
    public class NotificationMessage: Event
    {
        public int ApplicationId { get; set; }
        public string Message { get; set; }
        public DateTime? SubmitDate { get; set; }
    }
}
