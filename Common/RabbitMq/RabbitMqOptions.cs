namespace Common.RabbitMq
{
    /// <summary>
    /// Configuration object for RabbitMq
    /// </summary>
    public class RabbitMqOptions
    {
        public string Hostname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
    }
}
