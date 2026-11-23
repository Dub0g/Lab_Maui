using SleepHelper.Models;

namespace SleepHelper.Helpers;

/// Розрахунок часу відбою/підйому за 90-хвилинними циклами сну.
public static class SleepCycleCalculator
{
	/// Один цикл сну ≈ 90 хвилин, на засинання в середньому йде ≈ 15 хвилин.
	public const int CycleMin = 90;
	public const int FallAsleepMin = 15;
	public const int OptimalCycles = 5;

	/// Коли лягти, щоб прокинутись у час wake (наприкінці циклу).
	public static List<CycleResult> BedtimesForWake(DateTime wake)
		=> new[] { 6, 5, 4 }
			.Select(n => new CycleResult(n, n * CycleMin, wake.AddMinutes(-(n * CycleMin + FallAsleepMin))))
			.ToList();

	/// Коли прокинутись, якщо лягти у час bed.
	public static List<CycleResult> WakeTimesForBed(DateTime bed)
		=> new[] { 4, 5, 6 }
			.Select(n => new CycleResult(n, n * CycleMin, bed.AddMinutes(FallAsleepMin + n * CycleMin)))
			.ToList();
}
