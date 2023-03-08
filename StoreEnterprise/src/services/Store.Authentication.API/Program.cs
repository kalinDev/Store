using Store.Authentication.API.Extensions.Configurations;
using Store.Authentication.Infra.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.ResolveDependencies();
builder.Services.AddMessageBusConfiguration(builder.Configuration);

var app = builder.Build();

app.UseApiConfiguration();
app.UseSwaggerConfiguration(app.Environment);

app.Run();