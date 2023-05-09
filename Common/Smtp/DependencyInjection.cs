using Common.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Common.RabbitMq
{
    /// <summary>
    /// Wiring up smtp client configuration through the IoC Container
    /// </summary>
    public static class SmtpDependencyInjection
    {
        public static void AddSmtpClientConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var smtpConfiguration = configuration.GetSection("SmtpSettings");
            var smtpOptions = new SmtpSettings();
            smtpConfiguration.Bind(smtpOptions);
            services.AddSingleton(smtpOptions);
        }
    }
}
