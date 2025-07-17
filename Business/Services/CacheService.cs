using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Business.Services;

public class CacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken ct) where T : class
    {
        var cached = await _cache.GetStringAsync(key, ct);
        return cached is null
            ? null
            : JsonSerializer.Deserialize<T>(cached);
    }

    public Task SetAsync<T>(string key, T value, CancellationToken ct) where T : class
    {
        var json = JsonSerializer.Serialize(value);
        return _cache.SetStringAsync(key, json, ct);
    }

    public Task RemoveAsync(string key, CancellationToken ct)
    {
        return _cache.RemoveAsync(key, ct);
    }
}