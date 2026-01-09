using Ordering.Application.Services;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Cache;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository(IEventStore eventStore, ISnapshotCache<OrderSnapshot> snapshotCache) : IEventSourcedRepository<Order>
    {
        // ToDo move to Env variable 
        // Maybe enought TTL ???
        private const int SnapshotFrequency = 2;

        public async Task SaveAsync(Order aggregate)
        {
            var uncommittedEvents = aggregate.GetUncommittedEvents().ToList();
            if (!uncommittedEvents.Any())
            {
                return;
            }

            await eventStore.Append(
                aggregate.Id,
                uncommittedEvents,
                aggregate.Version - uncommittedEvents.Count);

            if (aggregate.Version % SnapshotFrequency == 0)
            {
                var snapshot = aggregate.CreateSnapshot();
                await snapshotCache.SetAsync(snapshot);
            }

            aggregate.ClearUncommittedEvents();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            Order aggregate = new Order();
            var fromVersion = 0;

            var snapshot = await snapshotCache.GetAsync(id);

            if (snapshot != null)
            {
                aggregate.RestoreFromSnapshot(snapshot);
                fromVersion = snapshot.Version + 1;
            }

            var events = await eventStore.Fetch(id, fromVersion);

            if (!events.Any() && snapshot == null)
            {
                return null;
            }

            aggregate.Load(events);
            return aggregate;
        }
    }
}
