using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Data.Repositorios;
using ProjControleFinanceiro.Domain.DTOs.Cartao;
using ProjControleFinanceiro.Domain.DTOs.Conta;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Domain.Services;
using ProjControleFinanceiro.Domain.Validators.Cartao;
using ProjControleFinanceiro.Domain.Validators.Conta;
using ProjControleFinanceiro.Domain.Validators.Transacao;

namespace ProjControleFinanceiro.Api.IoC
{
    public static class InjecaoDependenciaConfig
    {
        public static void RegistrarServicos(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContextoBase>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ICartaoRepository, CartaoRepository>();
            services.AddScoped<IFaturaRepository, FaturaRepository>();
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

            services.AddScoped<IContaService, ContaService>();
            services.AddScoped<ICartaoService, CartaoService>();
            services.AddScoped<ITransacaoService, TransacaoService>();

            services.AddScoped<IValidator<CartaoAddDTO>, CartaoAddValidator>();
            services.AddScoped<IValidator<CartaoUpdDTO>, CartaoUpdValidator>();
            services.AddScoped<IValidator<ContaUpdDTO>, ContaUpdValidator>();
            services.AddScoped<IValidator<ContaAddDTO>, ContaAddValidator>();
            services.AddScoped<IValidator<TransacaoAddDTO>, TransacaoAddValidator>();
        }
    }
}
