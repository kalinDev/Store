using Core.Util;
using Store.MessageBus;

namespace Store.Authentication.API.Extensions.Configurations;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
    }
}