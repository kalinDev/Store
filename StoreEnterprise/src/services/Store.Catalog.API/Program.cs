using Store.Catalog.API.Extensions.Configurations;
using Store.Catalog.Infra.CrossCutting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAutoMapperConfiguration();

builder.Services.ResolveDependencies();

var app = builder.Build();

app.UseApiConfiguration();
app.UseSwaggerConfiguration(app.Environment);

app.Run();