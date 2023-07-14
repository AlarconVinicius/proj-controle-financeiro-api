using Microsoft.Extensions.Logging;
using ProjControleFinanceiro.Mobile.IoC;

namespace ProjControleFinanceiro.Mobile;

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
			.RegisterDatabaseAndRepositories();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
