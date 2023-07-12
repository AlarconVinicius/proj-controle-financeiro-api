using LiteDB;
using ProjControleFinanceiro.Mobile.Configuracao;
using ProjControleFinanceiro.Mobile.Interfaces;
using ProjControleFinanceiro.Mobile.Repositories;
using ProjControleFinanceiro.Mobile.Views;

namespace ProjControleFinanceiro.Mobile.IoC
{
    public static  class DependencyInjectionConfig
    {

        public static MauiAppBuilder RegisterDatabaseAndRepositories(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<LiteDatabase>(
                options =>
                {
                    return new LiteDatabase($"Filename={AppSettings.DatabasePath};Connection=Shared");
                });

            builder.Services.AddTransient<ITransacaoRepository, TransacaoRepositoy>();

            return builder;
        }
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<TransactionList>();
            //builder.Services.AddTransient<TransactionAdd>();
            //builder.Services.AddTransient<TransactionEdit>();

            return builder;
        }
    }
}
