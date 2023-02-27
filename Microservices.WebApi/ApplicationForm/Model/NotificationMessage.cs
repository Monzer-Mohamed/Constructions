using RabbitMQ;
using System;

namespace ApplicationForm
{
    public class NotificationMessage: Event
    {
        public int ApplicationId { get; set; }
        public string Message { get; set; }
        public DateTime? SubmitDate { get; set; }
    }
}
