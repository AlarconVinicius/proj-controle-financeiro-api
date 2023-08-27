using Microsoft.AspNetCore.Mvc;

using ProjControleFinanceiro.Api.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDbContextServices(builder.Configuration);

builder.Services.ConfigureCustomServices();
builder.Services.AddApiConfiguration();
builder.Services.AddSwaggerConfiguration();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

app.UseApiConfiguration(app.Environment);

app.MigrationInitialisation();

app.Run();
