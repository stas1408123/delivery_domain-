using Microsoft.Extensions.Caching.Memory;

namespace Ordering.Infrastructure.Cache
{
    public class MemoryCacheRepository(IMemoryCache cache) : ICacheRepository
    {
        public Task SetAsync<T>(string key, T value)
        {
            cache.Set(key, value);
            return Task.CompletedTask;
        }

        public Task<T?> GetAsync<T>(string key)
        {
            cache.TryGetValue(key, out T? value);
            return Task.FromResult(value);
        }

        public Task RemoveAsync(string key)
        {
            cache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
