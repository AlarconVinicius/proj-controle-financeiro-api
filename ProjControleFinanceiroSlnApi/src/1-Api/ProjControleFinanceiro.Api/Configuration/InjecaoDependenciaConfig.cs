using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjControleFinanceiro.Api.Extensions;
using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Data.Repositorios;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.DTOs.Transacao.Relatorio;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Domain.Services;
using ProjControleFinanceiro.Domain.Validators.Transacao;
using ProjControleFinanceiro.Domain.Validators.Transacao.Relatorio;

namespace ProjControleFinanceiro.Api.Configuration
{
    public static class InjecaoDependenciaConfig
    {
        public static void RegistrarServicos(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContextoBase>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

            services.AddScoped<IMainService, MainService>();
            services.AddScoped<ITransacaoService, TransacaoService>();

            services.AddScoped<IValidator<TransacaoAddDTO>, TransacaoAddValidator>();
            services.AddScoped<IValidator<TransacaoUpdDTO>, TransacaoUpdValidator>();
            services.AddScoped<IValidator<RelatorioPDF>, RelatorioAddValidator>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
