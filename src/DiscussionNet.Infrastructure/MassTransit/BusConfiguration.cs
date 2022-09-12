using DiscussionNet.Infrastructure.RabbitMQ;
using DiscussionNet.Infrastructure.Utilities.IoC;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscussionNet.Infrastructure.MassTransit
{
    public static class BusConfiguration
    {
        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator> registrationAction = null)
        {
            var configuration = ServiceLocator.ServiceProvider.GetService<IConfiguration>();
            var credentials = configuration.GetSection("RabbitMQ").Get<RabbitMQCredentials>();

            return Bus.Factory.CreateUsingRabbitMq(configuration =>
            {
                configuration.Host(credentials.HostName, hostConfiguration =>
                {
                    hostConfiguration.Username(credentials.Username);
                    hostConfiguration.Password(credentials.Password);
                });

                registrationAction?.Invoke(configuration);
            });
        }
    }
}
