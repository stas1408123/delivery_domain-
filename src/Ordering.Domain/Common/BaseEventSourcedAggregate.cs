namespace Ordering.Domain.Common
{
    public abstract class BaseEventSourcedAggregate : IEventSourcedAggregate
    {
        public Guid Id { get; protected set; }
        public int Version { get; protected set; } = 0;

        private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();

        public IReadOnlyCollection<IEvent> GetUncommittedEvents() => _uncommittedEvents.AsReadOnly();

        public void ClearUncommittedEvents() => _uncommittedEvents.Clear();

        public void Load(IEnumerable<IEvent> history)
        {
            foreach (var @event in history.OrderBy(e => e.AggregateVersion))
            {
                Version = @event.AggregateVersion;
                Apply(@event);
            }
        }

        public void AppendEvent(IEvent @event)
        {
            Version++;                         
            @event.AggregateVersion = Version;
            Apply(@event);
            _uncommittedEvents.Add(@event);
        }

        public abstract void Apply(IEvent @event);
    }
}
