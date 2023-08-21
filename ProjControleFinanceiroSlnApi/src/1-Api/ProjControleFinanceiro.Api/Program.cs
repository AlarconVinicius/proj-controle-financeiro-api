using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ProjControleFinanceiro.Api.IoC;
using ProjControleFinanceiro.Data.Configuracao;

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
using var scope = app.Services.CreateScope();
var dbContextBase = scope.ServiceProvider.GetRequiredService<ContextoBase>();
var dbContextIdentity = scope.ServiceProvider.GetRequiredService<ContextoBase>();
if (dbContextBase.Database.GetPendingMigrations().Any())
{
    dbContextBase.Database.Migrate();
}
if (dbContextIdentity.Database.GetPendingMigrations().Any())
{
    dbContextIdentity.Database.Migrate();
}

app.Run();
