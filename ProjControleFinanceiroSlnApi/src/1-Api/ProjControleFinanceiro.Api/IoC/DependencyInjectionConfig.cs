using System.Text;

using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Data.Repositorios;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.DTOs.Transacao.Relatorio;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Domain.Services;
using ProjControleFinanceiro.Domain.Services.Configuracao;
using ProjControleFinanceiro.Domain.Validators.Transacao;
using ProjControleFinanceiro.Domain.Validators.Transacao.Relatorio;
using ProjControleFinanceiro.Identity.Configuracao;

namespace ProjControleFinanceiro.Api.IoC;

public static class DependencyInjectionConfig
{
    public static void ConfigureDbContextServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ContextoBase>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDefaultIdentity<IdentityUser>()
           .AddRoles<IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders()
           .AddErrorDescriber<IdentityMensagensPortugues>();

        var appSettingsSection = configuration.GetSection("AppSettings");

        //Aqui: O middleware entende que a classe AppSettings represente os dados da sessão AppSettings (ou seja, os dados)
        services.Configure<AppSettings>(appSettingsSection);


        var appSettings = appSettingsSection.Get<AppSettings>();
        var key = Encoding.ASCII.GetBytes(appSettings!.Secret);

        //JWT
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOptions =>
        {
            bearerOptions.RequireHttpsMetadata = true;
            bearerOptions.SaveToken = true;
            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = appSettings.ValidoEm,
                ValidIssuer = appSettings.Emissor
            };
        });
    }
    public static void ConfigureCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddScoped<ITransacaoRepository, TransacaoRepository>();
        services.AddScoped<IRelatorioRepository, RelatorioRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

        services.AddScoped<IMainService, MainService>();
        services.AddScoped<ITransacaoService, TransacaoService>();
        services.AddScoped<IRelatorioService, RelatorioService>();
        services.AddScoped<IEnumService, EnumService>();

        services.AddScoped<IValidator<TransacaoAddDto>, TransacaoAddValidator>();
        services.AddScoped<IValidator<TransacaoUpdDto>, TransacaoUpdValidator>();
        services.AddScoped<IValidator<RelatorioPdfDto>, RelatorioAddValidator>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();
    }

    //public static void ConfigureJwtServices(IServiceCollection services, IConfiguration configuration)
    //{
    //    var appSettingsSection = configuration.GetSection("AppSettings");

    //    //Aqui: O middleware entende que a classe AppSettings represente os dados da sessão AppSettings (ou seja, os dados)
    //    services.Configure<AppSettings>(appSettingsSection);


    //    var appSettings = appSettingsSection.Get<AppSettings>();
    //    var key = Encoding.ASCII.GetBytes(appSettings!.Secret);

    //    //JWT
    //    services.AddAuthentication(options =>
    //    {
    //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //    }).AddJwtBearer(bearerOptions =>
    //    {
    //        bearerOptions.RequireHttpsMetadata = true;
    //        bearerOptions.SaveToken = true;
    //        bearerOptions.TokenValidationParameters = new TokenValidationParameters
    //        {
    //            ValidateIssuerSigningKey = true,
    //            IssuerSigningKey = new SymmetricSecurityKey(key),
    //            ValidateIssuer = true,
    //            ValidateAudience = true,
    //            ValidAudience = appSettings.ValidoEm,
    //            ValidIssuer = appSettings.Emissor
    //        };
    //    });
    //}
}
