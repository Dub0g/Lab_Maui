using System.Collections.ObjectModel;
using SleepHelper.Models;

namespace SleepHelper.Services;

/// Стан щоденника сну (аналог SleepProvider): список записів + синхронізація.
public class SleepStore(ISleepApi api)
{
	public ObservableCollection<SleepEntry> Entries { get; } = new();

	/// Зберігає запис локально й намагається надіслати на сервер.
	/// Якщо мережі немає — запис усе одно зберігається (Synced = false).
	/// Повертає текст помилки або null, якщо все добре.
	public async Task<string?> AddEntryAsync(SleepEntry draft)
	{
		var id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
		try
		{
			var remoteId = await api.PostEntryAsync(draft);
			Entries.Insert(0, draft with { Id = id, RemoteId = remoteId, Synced = true });
			return null;
		}
		catch (Exception e)
		{
			Entries.Insert(0, draft with { Id = id, Synced = false });
			return e is SleepApiException ? e.Message : "Немає з’єднання";
		}
	}
}
