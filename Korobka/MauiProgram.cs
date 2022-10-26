using CommunityToolkit.Maui;
using Korobka.Models;
using Korobka.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Korobka;

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
			});

		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<AppShellViewModel>(); 
        builder.UseMauiApp<App>();
        builder.UseMauiCommunityToolkit();

        return builder.Build();
	}
}
