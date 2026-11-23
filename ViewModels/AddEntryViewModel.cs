using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SleepHelper.Helpers;
using SleepHelper.Models;
using SleepHelper.Services;

namespace SleepHelper.ViewModels;

public partial class AddEntryViewModel : ObservableObject
{
	private readonly SleepStore _store;
	private int _quality;

	public AddEntryViewModel(SleepStore store)
	{
		_store = store;
		QualityOptions = Qualities.All.Select((q, i) => new QualityOptionViewModel(i, q)).ToList();
		SelectQuality(3);
	}

	public List<QualityOptionViewModel> QualityOptions { get; }

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(DurationText))]
	private TimeSpan bedtime = new(23, 30, 0);

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(DurationText))]
	private TimeSpan wake = new(7, 15, 0);

	[ObservableProperty] private string notes = "";
	[ObservableProperty] private string qualityLabel = "";

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(NotSaving))]
	private bool saving;

	public bool NotSaving => !Saving;

	public string DurationText =>
		$"Тривалість: {TimeFormat.Duration(DurationCalculator.Minutes(Bedtime, Wake))}";

	[RelayCommand]
	private void SelectQuality(int index)
	{
		_quality = index;
		foreach (var q in QualityOptions) q.IsActive = q.Index == index;
		QualityLabel = QualityOptions[index].Label;
	}

	[RelayCommand]
	private async Task Save()
	{
		if (Saving) return;
		Saving = true;

		var entry = new SleepEntry(
			Id: "",
			Date: TimeFormat.TodayKey(),
			Bedtime: TimeFormat.Time(Bedtime),
			Wake: TimeFormat.Time(Wake),
			DurationMin: DurationCalculator.Minutes(Bedtime, Wake),
			Quality: _quality,
			Notes: Notes.Trim());

		var error = await _store.AddEntryAsync(entry);
		Saving = false;

		await Shell.Current.GoToAsync("..");
		if (error != null)
		{
			await Shell.Current.DisplayAlert("Синхронізація",
				$"Не вдалося синхронізувати ({error}). Запис збережено локально.", "OK");
		}
	}
}
