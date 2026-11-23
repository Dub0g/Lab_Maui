using System.Net.Http.Json;
using System.Text.Json;
using SleepHelper.Models;

namespace SleepHelper.Services;

/// HTTP-клієнт для тестового REST-сервера (jsonplaceholder).
public sealed class SleepApiClient : ISleepApi
{
	private const string Endpoint = "https://jsonplaceholder.typicode.com/posts";
	private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(15) };

	public async Task<int?> PostEntryAsync(SleepEntry entry, CancellationToken ct = default)
	{
		var payload = new
		{
			title = $"sleep-entry-{entry.Date}",
			body = JsonSerializer.Serialize(new
			{
				date = entry.Date,
				bedtime = entry.Bedtime,
				wake = entry.Wake,
				durationMin = entry.DurationMin,
				quality = entry.Quality,
				notes = entry.Notes,
			}),
			userId = 1,
		};

		using var res = await Http.PostAsJsonAsync(Endpoint, payload, ct);
		if (!res.IsSuccessStatusCode)
			throw new SleepApiException($"Помилка сервера: {(int)res.StatusCode}");

		using var doc = JsonDocument.Parse(await res.Content.ReadAsStringAsync(ct));
		return doc.RootElement.TryGetProperty("id", out var idEl) && idEl.TryGetInt32(out var id) ? id : null;
	}
}
