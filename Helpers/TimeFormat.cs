using System.Globalization;

namespace SleepHelper.Helpers;

/// Форматування часу, тривалості та дати для відображення й збереження.
public static class TimeFormat
{
	public static string Time(TimeSpan t) => $"{t.Hours:00}:{t.Minutes:00}";

	public static string Time(DateTime d) => $"{d.Hour:00}:{d.Minute:00}";

	public static string Duration(int minutes)
	{
		var h = minutes / 60;
		var m = minutes % 60;
		if (h == 0) return $"{m} хв";
		if (m == 0) return $"{h} год";
		return $"{h} год {m} хв";
	}

	/// Сьогоднішня дата у форматі YYYY-MM-DD (за місцевим часом).
	public static string TodayKey() => DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
}
