using Ordering.Application.Services;
using Ordering.Domain.Common;

namespace Ordering.Infrastructure.EventStore
{
    public class EventSourcedRepository<T> : IEventSourcedRepository<T>
        where T : BaseEventSourcedAggregate, new()
    {
        private readonly IEventStore _eventStore;

        public EventSourcedRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task SaveAsync(T aggregate)
        {
            var uncommittedEvents = aggregate.GetUncommittedEvents().ToList();
            if (!uncommittedEvents.Any())
            {
                return;
            }

            await _eventStore.Append(aggregate.Id, uncommittedEvents, aggregate.Version - uncommittedEvents.Count);
            aggregate.ClearUncommittedEvents();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var events = await _eventStore.Fetch(id);
            if (!events.Any())
            {
                return null;
            }

            var aggregate = new T();
            aggregate.Load(events);
            return aggregate;
        }
    }
}
