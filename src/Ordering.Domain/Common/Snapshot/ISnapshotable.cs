using Ordering.Domain.Common.Snapshot;

namespace Ordering.Application.Common.Interfaces
{
    public interface ISnapshotable<T>
    where T : ISnapshot
    {
        T CreateSnapshot();
        void RestoreFromSnapshot(T snapshot);
    }
}
