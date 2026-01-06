using Ordering.Domain.Common;

namespace Ordering.Infrastructure.Cache
{
    public class SnapshotCache<T>(ICacheRepository cache) : ISnapshotCache<T>
    where T : BaseSnapshot
    {
        public Task<T?> GetAsync(Guid aggregateId)
        {
            var key = BuildKey(aggregateId);
            return cache.GetAsync<T>(key);
        }

        public Task SetAsync(T snapshot)
        {
            var key = BuildKey(snapshot.AggregateId);
            return cache.SetAsync(key, snapshot);
        }

        private static string BuildKey(Guid aggregateId)
            => $"{typeof(T).Name}:{aggregateId}";
    }
}
