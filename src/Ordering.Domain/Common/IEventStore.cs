namespace Ordering.Domain.Common
{
    public interface IEventStore
    {
        Task<IEnumerable<IEvent>> Fetch(Guid aggregatedId);
        Task Append(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion);
    }
}
