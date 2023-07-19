using LiteDB;
using ProjCF.Mobile.Data.Configuration;
using ProjCF.Mobile.Data.Repositories;
using ProjCF.Mobile.Domain.Interfaces.Repositories;
using ProjCF.Mobile.Views;

namespace ProjCF.Mobile.IoC
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

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<TransacaoListView>();
            builder.Services.AddTransient<TransacaoAddView>();
            builder.Services.AddTransient<TransacaoEditView>();

            return builder;
        }
    }
}
