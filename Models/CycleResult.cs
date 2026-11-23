using SleepHelper.Helpers;

namespace SleepHelper.Models;

/// Один варіант із калькулятора сну: скільки циклів і на яку годину.
public record CycleResult(int Cycles, int SleepMin, DateTime Time)
{
	private static readonly Brush AccentBrush = new SolidColorBrush(Color.FromArgb("#FBBF24"));
	private static readonly Brush LineBrush = new SolidColorBrush(Color.FromArgb("#2A3447"));
	private static readonly Color AccentSoft = Color.FromArgb("#2EFBBF24");
	private static readonly Color InputBg = Color.FromArgb("#111827");
	private static readonly Color Accent = Color.FromArgb("#FBBF24");
	private static readonly Color Muted = Color.FromArgb("#8B95A7");

	public string TimeText => TimeFormat.Time(Time);
	public string Description => $"{Cycles} {UkrainianGrammar.CyclesWord(Cycles)} ({TimeFormat.Duration(SleepMin)} сну)";
	public bool IsOptimal => Cycles == SleepCycleCalculator.OptimalCycles;
	public string Badge => Cycles switch { 4 => "Мінімум", 5 => "Оптимально", 6 => "Повноцінно", _ => "" };

	public Brush BorderBrush => IsOptimal ? AccentBrush : LineBrush;
	public Color BadgeBg => IsOptimal ? AccentSoft : InputBg;
	public Color BadgeText => IsOptimal ? Accent : Muted;
}
