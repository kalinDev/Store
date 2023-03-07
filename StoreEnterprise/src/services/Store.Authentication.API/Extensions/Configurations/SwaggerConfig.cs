using Microsoft.OpenApi.Models;

namespace Store.Authentication.API.Extensions.Configurations;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Store Authentication API",
                Version = "v1",
                Contact = new OpenApiContact{ Name = "Kalin dev", Email = "vasconcelosjoao438@gmail.com"}
            });

        });
    }
    
    public static void UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store Authentication API");
        });
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}