using LiteDB;
using ProjControleFinanceiro.Mobile.Data.Configuration;
using ProjControleFinanceiro.Mobile.Data.Repositories;
using ProjControleFinanceiro.Mobile.Domain.Interfaces.Repositories;

namespace ProjControleFinanceiro.Mobile.IoC
{
    public static class DependencyInjectionConfig
    {

        public static MauiAppBuilder RegisterDatabaseAndRepositories(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<LiteDatabase>(
            options =>
            {
                return new LiteDatabase($"Filename={Configuracao.DatabasePath};Connection=Shared");
            });

            builder.Services.AddTransient<ITransacaoAppRepository, TransacaoAppRepository>();

            return builder;
        }
    }
}
