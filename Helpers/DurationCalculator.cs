namespace SleepHelper.Helpers;

public static class DurationCalculator
{
	/// Тривалість сну в хвилинах; якщо підйом «раніше» за відбій — вважаємо, що це наступна доба.
	public static int Minutes(TimeSpan bed, TimeSpan wake)
	{
		var bedMin = bed.Hours * 60 + bed.Minutes;
		var wakeMin = wake.Hours * 60 + wake.Minutes;
		var diff = wakeMin - bedMin;
		if (diff <= 0) diff += 24 * 60;
		return diff;
	}
}
