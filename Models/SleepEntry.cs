using SleepHelper.Helpers;

namespace SleepHelper.Models;

/// Запис у щоденнику сну.
public record SleepEntry(
	string Id,
	string Date,
	string Bedtime,
	string Wake,
	int DurationMin,
	int Quality,
	string Notes = "",
	int? RemoteId = null,
	bool Synced = false)
{
	private static readonly Color SyncedColor = Color.FromArgb("#34D399");
	private static readonly Color LocalColor = Color.FromArgb("#8B95A7");

	// Властивості для відображення у списку
	public string Range => $"{Bedtime} – {Wake}";
	public string Meta => $"{TimeFormat.Duration(DurationMin)} · {Date}";
	public bool HasNotes => !string.IsNullOrEmpty(Notes);
	public string QualityEmoji => Qualities.At(Quality).Emoji;
	public string SyncGlyph => Synced ? "☁✓" : "☁✗";
	public Color SyncColor => Synced ? SyncedColor : LocalColor;
}
