namespace Ordering.Infrastructure.Cache
{
    public interface ICacheRepository
    {
        Task SetAsync<T>(string key, T value);

        Task<T?> GetAsync<T>(string key);

        Task RemoveAsync(string key);
    }
}
