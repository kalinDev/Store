using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Authentication.Data;
using Store.WebApi.Core.Identity;

namespace Store.Authentication.API.Extensions.Configurations;

public static class IdentityConfig
{
    public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddIdentity(services);
        
        services.AddJwtConfiguration(configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApiDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    private static void AddIdentity(IServiceCollection services)
    {
        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApiDbContext>()
            .AddDefaultTokenProviders();
    }

}