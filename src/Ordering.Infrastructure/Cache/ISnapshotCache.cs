using Ordering.Domain.Common;

namespace Ordering.Infrastructure.Cache
{
    public interface ISnapshotCache<T>
    where T : BaseSnapshot
    {
        Task<T?> GetAsync(Guid aggregateId);
        Task SetAsync(T snapshot);
    }
}
