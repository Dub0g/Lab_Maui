namespace SleepHelper.Helpers;

public static class UkrainianGrammar
{
	/// Правильна форма слова: 1 цикл, 2–4 цикли, 5+ циклів.
	public static string CyclesWord(int n)
	{
		var last = n % 10;
		var lastTwo = n % 100;
		if (lastTwo is >= 11 and <= 14) return "циклів";
		if (last == 1) return "цикл";
		if (last is >= 2 and <= 4) return "цикли";
		return "циклів";
	}
}
