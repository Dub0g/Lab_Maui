using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SleepHelper.Helpers;
using SleepHelper.Services;

namespace SleepHelper.ViewModels;

public partial class HomeViewModel : ObservableObject
{
	private readonly SleepStore _store;

	public HomeViewModel(SleepStore store)
	{
		_store = store;
		_store.Entries.CollectionChanged += OnEntriesChanged;
		Refresh();
	}

	private void OnEntriesChanged(object? sender, NotifyCollectionChangedEventArgs e) => Refresh();

	public string Greeting
	{
		get
		{
			var h = DateTime.Now.Hour;
			if (h >= 5 && h < 12) return "Доброго ранку,";
			if (h >= 12 && h < 18) return "Добрий день,";
			if (h >= 18 && h < 23) return "Добрий вечір,";
			return "Доброї ночі,";
		}
	}

	public string UserName => "Богдан";
	public string UserInitial => UserName[..1];

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(NoData))]
	private bool hasData;

	public bool NoData => !HasData;

	[ObservableProperty] private string percentText = "—";
	[ObservableProperty] private double progress;
	[ObservableProperty] private string summary = "";

	private void Refresh()
	{
		// Статистика за останні 7 записів
		var recent = _store.Entries.Take(7).ToList();
		HasData = recent.Count > 0;
		if (!HasData) return;

		var avgQuality = recent.Average(e => e.Quality);
		var percent = (int)Math.Round((avgQuality + 1) / 5 * 100, MidpointRounding.AwayFromZero);
		var avgMin = (int)Math.Round(recent.Average(e => e.DurationMin), MidpointRounding.AwayFromZero);

		PercentText = percent.ToString();
		Progress = Math.Clamp(percent / 100.0, 0, 1);
		Summary = $"Середня оцінка за останні записи ({recent.Count}). " +
		          $"Середня тривалість сну: {TimeFormat.Duration(avgMin)}.";
	}

	[RelayCommand]
	private Task Go(string route) => Shell.Current.GoToAsync($"//{route}");
}
