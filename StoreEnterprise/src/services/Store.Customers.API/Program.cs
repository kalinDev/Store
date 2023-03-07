using System.Reflection;
using Store.Client.Infra.CrossCutting.IoC;
using Store.Customers.API.Extensions.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddAutoMapperConfiguration();

builder.Services.ResolveDependencies();

var app = builder.Build();

app.UseApiConfiguration();
app.UseSwaggerConfiguration(app.Environment);

app.Run();