using CommunityToolkit.Maui;
using DailyPoetry.Views;

namespace DailyPoetry;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<PoetryViewModel>();
        builder.Services.AddSingleton<ResultPage>();
        builder.Services.AddSingleton<ResultPageViewModel>();

		builder.Services.AddSingleton<IPreferenceStorage, PreferenceStorage>();
        builder.Services.AddSingleton<IPoetryStorage, PoetryStorage>();
		
        return builder.Build();
	}
}
