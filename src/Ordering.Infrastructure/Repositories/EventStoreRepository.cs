using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Entities;
using Ordering.Infrastructure.Serializer;
using System.Data;

namespace Ordering.Infrastructure.Repositories
{
    public class EventStoreRepository : IEventStore
    {
        private readonly OrderingDbContext _context;
        private readonly IEventStoreSerializer _serializer;

        public EventStoreRepository(OrderingDbContext context, IEventStoreSerializer serializer)
        {
            _context = context;
            _serializer = serializer;
        }

        public async Task Append(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion)
        {
            var existingEvents = await _context.Events
                .Where(e => e.AggregateId == aggregateId)
                .OrderByDescending(e => e.AggregateVersion)
                .Select(e => e.AggregateVersion)
                .FirstOrDefaultAsync();

            var currentVersion = existingEvents;

            if (currentVersion != expectedVersion && currentVersion != 0)
            {
                throw new DbUpdateConcurrencyException($"Aggregate {aggregateId} has been modified concurrently. Expected version: {expectedVersion}, Current version: {currentVersion}");
            }

            var eventEntities = new List<StoredEvent>();
            foreach (var @event in events)
            {
                eventEntities.Add(new StoredEvent
                {
                    AggregateId = @event.AggregateId,
                    AggregateType = @event.GetType().Name,
                    AggregateVersion = @event.AggregateVersion,
                    Data = _serializer.Serialize(@event),
                    Timestamp = @event.CreatedAt,
                    EventType = @event.GetType().AssemblyQualifiedName 
                });
            }

            await _context.Events.AddRangeAsync(eventEntities);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IEvent>> Fetch(Guid aggregateId)
        {
            var storedEvents = await _context.Events
                .Where(e => e.AggregateId == aggregateId)
                .OrderBy(e => e.AggregateVersion)
                .ToListAsync();

            if (!storedEvents.Any())
            {
                return new List<IEvent>();
            }

            var domainEvents = new List<IEvent>();
            foreach (var storedEvent in storedEvents)
            {
                var eventData = _serializer.Deserialize(storedEvent.Data, Type.GetType(storedEvent.EventType));
                if (eventData is IEvent domainEvent)
                {
                    domainEvent.AggregateVersion = storedEvent.AggregateVersion; // Ensure version is set for rehydration
                    domainEvents.Add(domainEvent);
                }
            }
            return domainEvents;
        }
    }

}
