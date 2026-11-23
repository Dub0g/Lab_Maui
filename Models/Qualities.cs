namespace SleepHelper.Models;

/// Довідник оцінок якості сну (індекс 0–4).
public static class Qualities
{
	public static readonly IReadOnlyList<Quality> All = new[]
	{
		new Quality("😫", "Жахливо"),
		new Quality("😕", "Погано"),
		new Quality("😐", "Нормально"),
		new Quality("🙂", "Добре"),
		new Quality("😍", "Чудово"),
	};

	public static Quality At(int index) => All[Math.Clamp(index, 0, All.Count - 1)];
}
