using Ordering.Domain.Common;
using Ordering.Domain.Common.Snapshot;

namespace Ordering.Infrastructure.Cache
{
    public interface ISnapshotCache<T>
    where T : BaseSnapshot
    {
        Task<T?> GetAsync(Guid aggregateId);
        Task SetAsync(T snapshot);
    }
}
