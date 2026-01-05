namespace Ordering.Domain.Common
{
    public interface IEventSourcedAggregate
    {
        Guid Id { get; }
        int Version { get; }
        IReadOnlyCollection<IEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();
        void AppendEvent(IEvent @event);
        void Apply(IEvent @event);
        void Load(IEnumerable<IEvent> history);
    }
}
