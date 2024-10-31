using Microsoft.Extensions.Caching.Memory;

namespace QMan.Application.Services.Cache;

public class MemoryCacheService(IMemoryCache memoryCache) : ICacheService
{
    public T? Get<T>(string key) => memoryCache.TryGetValue<T>(key, out var value) ? value : default;


    public void Set(string key, object? value, TimeSpan? expiration) =>
        memoryCache.Set(key, value, expiration ?? TimeSpan.FromDays(1));


    public void Remove(string key) => memoryCache.Remove(key);


    public void Clear() => memoryCache.Dispose();
}