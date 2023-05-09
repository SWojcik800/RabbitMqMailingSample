using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Common.RabbitMq
{
    /// <summary>
    /// Wiring up message bus configuration through the IoC Container
    /// </summary>
    public static class DependencyInjection
    {
        public static void AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqSection = configuration.GetSection("RabbitMQ");
            var rabbitMqOptions = new RabbitMqOptions();
            rabbitMqSection.Bind(rabbitMqOptions);
            services.AddSingleton(rabbitMqOptions);

            services.AddSingleton(sp =>
            {
                var options = sp.GetService<RabbitMqOptions>();

                var factory = new ConnectionFactory
                {
                    HostName = options.Hostname,
                    UserName = options.Username,
                    Password = options.Password,
                    VirtualHost = options.VirtualHost,
                    Port = options.Port
                };

                return factory.CreateConnection();
            });


            services.AddSingleton<IMessageBus, RabbitMqMessageBus>();
        }
    }
}
