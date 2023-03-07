using Microsoft.AspNetCore.Mvc;
using Store.WebApi.Core.Identity;

namespace Store.Authentication.API.Extensions.Configurations;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    }
    
    public static void UseApiConfiguration(this IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseAuthConfiguration();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}