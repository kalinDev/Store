using Store.Catalog.Application.AutoMapper;

namespace Store.Catalog.API.Extensions.Configurations;

public static class AutoMapperConfiguration
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(typeof(AutoMapperProfile));
    }
}