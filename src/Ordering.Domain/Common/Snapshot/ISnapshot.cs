namespace Ordering.Domain.Common.Snapshot
{
    public interface ISnapshot
    {
        Guid AggregateId { get; }
        int Version { get; }
    }
}
