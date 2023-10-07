using PetsLocatorApp.Services;
using PetsLocatorApp.View;
using PetsLocatorApp.ViewModel;

namespace PetsLocatorApp;

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

		builder.Services.AddScoped<IPetService, PetService>();
		builder.Services.AddSingleton<PetsViewModel>();

		builder.Services.AddTransient<PetDetailsViewModel>();
		builder.Services.AddTransient<DetailsPage>();
		
		return builder.Build();
	}
}
