using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Catalog.Data;
using Store.WebApi.Core.Identity;

namespace Store.Catalog.API.Extensions.Configurations;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        
        services.AddJwtConfiguration(configuration);

        services.AddControllers();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    }
    
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        services.AddDbContext<CatalogContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void UseApiConfiguration(this IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseCors("Total");
        app.UseAuthConfiguration();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}