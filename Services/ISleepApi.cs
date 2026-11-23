using SleepHelper.Models;

namespace SleepHelper.Services;

/// Відправка записів на віддалений сервер.
public interface ISleepApi
{
	/// Повертає id запису на сервері. Кидає <see cref="SleepApiException"/>, якщо сервер відповів помилкою.
	Task<int?> PostEntryAsync(SleepEntry entry, CancellationToken ct = default);
}
