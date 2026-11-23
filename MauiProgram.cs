using System.Globalization;
using SleepHelper.Services;
using SleepHelper.ViewModels;
using SleepHelper.Views;

namespace SleepHelper;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var culture = new CultureInfo("uk-UA");
		CultureInfo.DefaultThreadCurrentCulture = culture;
		CultureInfo.DefaultThreadCurrentUICulture = culture;

		var builder = MauiApp.CreateBuilder();
		builder.UseMauiApp<App>();

		// Стан щоденника (аналог SleepProvider)
		builder.Services.AddSingleton<ISleepApi, SleepApiClient>();
		builder.Services.AddSingleton<SleepStore>();

		builder.Services.AddTransient<HomeViewModel>();
		builder.Services.AddTransient<DiaryViewModel>();
		builder.Services.AddTransient<CalcViewModel>();
		builder.Services.AddTransient<AddEntryViewModel>();

		builder.Services.AddTransient<HomePage>();
		builder.Services.AddTransient<DiaryPage>();
		builder.Services.AddTransient<StatsPage>();
		builder.Services.AddTransient<CalcPage>();
		builder.Services.AddTransient<RelaxPage>();
		builder.Services.AddTransient<AddEntryPage>();

		return builder.Build();
	}
}
