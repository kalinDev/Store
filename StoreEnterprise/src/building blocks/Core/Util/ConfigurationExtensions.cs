﻿using Microsoft.Extensions.Configuration;

namespace Core.Util;

public static class ConfigurationExtensions
{
    public static string GetMessageQueueConnection(this IConfiguration configuration, string name)
    {
        return configuration?.GetSection("MessageQueueConnection")?[name];
    }
}