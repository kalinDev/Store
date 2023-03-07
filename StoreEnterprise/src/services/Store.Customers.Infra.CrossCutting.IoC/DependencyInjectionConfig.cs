using Core.MediatR;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Customers.Application.Commands;
using Store.Customers.Application.Events;
using Store.Customers.Data;
using Store.Customers.Data.Repositories;
using Store.Customers.Domain.Interfaces;
using Store.Customers.Service;
using Store.Customers.Service.Services;

namespace Store.Client.Infra.CrossCutting.IoC;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, CustomerCommandHandler>();

        services.AddScoped<INotificationHandler<RegisteredCustomerEvent>, CustomerEventHandler>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomersContext>();

        services.AddHostedService<RegisterCustomerIntegrationHandler>();

    }
}