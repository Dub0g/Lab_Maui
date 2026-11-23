using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SleepHelper.Helpers;
using SleepHelper.Models;

namespace SleepHelper.ViewModels;

public partial class CalcViewModel : ObservableObject
{
	private static readonly Color ActiveBg = Color.FromArgb("#111827");
	private TimeSpan _wakeTime = new(7, 0, 0);
	private TimeSpan _bedTime = DateTime.Now.TimeOfDay;

	public string CycleSubtitle => $"Розрахунок {SleepCycleCalculator.CycleMin}-хвилинних циклів";
	public string FooterNote =>
		$"Враховано {SleepCycleCalculator.FallAsleepMin} хв на засинання. Прокидатися наприкінці циклу легше, ніж посеред нього.";

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(Time), nameof(TimeCaption), nameof(TimeGlyph),
		nameof(ResultsHeader), nameof(WakeTabBg), nameof(BedTabBg),
		nameof(WakeTabText), nameof(BedTabText))]
	private bool isWake = true;

	public TimeSpan Time
	{
		get => IsWake ? _wakeTime : _bedTime;
		set
		{
			if (Time == value) return;
			if (IsWake) _wakeTime = value; else _bedTime = value;
			OnPropertyChanged();
			ClearResults();
		}
	}

	public string TimeCaption => IsWake ? "Встановлено час підйому" : "Встановлено час відбою";
	public string TimeGlyph => IsWake ? "⏰" : "🛏";
	public string ResultsHeader => IsWake ? "РЕКОМЕНДОВАНО ЛЯГТИ:" : "РЕКОМЕНДОВАНО ПРОКИНУТИСЬ:";

	public Color WakeTabBg => IsWake ? ActiveBg : Colors.Transparent;
	public Color BedTabBg => IsWake ? Colors.Transparent : ActiveBg;
	public Color WakeTabText => IsWake ? Colors.White : Color.FromArgb("#8B95A7");
	public Color BedTabText => IsWake ? Color.FromArgb("#8B95A7") : Colors.White;

	public ObservableCollection<CycleResult> Results { get; } = new();

	[ObservableProperty] private bool hasResults;

	private void ClearResults()
	{
		Results.Clear();
		HasResults = false;
	}

	[RelayCommand]
	private void SetMode(string mode)
	{
		IsWake = mode == "wake";
		ClearResults();
	}

	[RelayCommand]
	private void Calculate()
	{
		var baseTime = DateTime.Today.Add(Time);
		var list = IsWake ? SleepCycleCalculator.BedtimesForWake(baseTime) : SleepCycleCalculator.WakeTimesForBed(baseTime);

		Results.Clear();
		foreach (var r in list) Results.Add(r);
		HasResults = true;
	}
}
