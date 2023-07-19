using Microsoft.Extensions.Logging;
using ProjCF.Mobile.IoC;

namespace ProjCF.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterDatabaseAndRepositories()
            .RegisterViews();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

