using Microsoft.Extensions.DependencyInjection;
using Store.Authentication.Data;

namespace Store.Authentication.Infra.CrossCutting.IoC;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection service)
    {
        service.AddScoped<ApiDbContext>();
    }
}