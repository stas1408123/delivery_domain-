namespace Ordering.Domain.Common
{
    public interface ISnapshot
    {
        Guid AggregateId { get; }
        int Version { get; }
    }
}
