using Ordering.Domain.Common;

namespace Ordering.Application.Common.Interfaces
{
    public interface ISnapshotable<T>
    where T : ISnapshot
    {
        T CreateSnapshot();
        void RestoreFromSnapshot(T snapshot);
    }
}
