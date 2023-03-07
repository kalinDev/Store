using Core.DomainObjects.Entities;
using Core.DomainObjects.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Store.Catalog.Application.Services;
using Store.Catalog.Data;
using Store.Catalog.Data.Repositories;
using Store.Catalog.Domain.Interfaces;

namespace Store.Catalog.Infra.CrossCutting;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<INotifier, Notifier>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<CatalogContext>();
    }
}