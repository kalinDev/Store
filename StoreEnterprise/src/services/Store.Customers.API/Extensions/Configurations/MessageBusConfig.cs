using Core.Util;
using Store.Customers.Service.Services;
using Store.MessageBus;

namespace Store.Customers.API.Extensions.Configurations;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<RegisterCustomerIntegrationHandler>();
    }
}