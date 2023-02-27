using System;

namespace RabbitMQ
{
  
    public class CofigurationSettingModel
    {
        public string Hostname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
    }
}
