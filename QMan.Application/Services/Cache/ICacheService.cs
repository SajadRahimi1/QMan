namespace QMan.Application.Services.Cache;

public interface ICacheService
{
    T? Get<T>(string key);
    void Set(string key, object? value, TimeSpan? expiration);
    void Remove(string key);
    void Clear();
}