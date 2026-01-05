using Ordering.Domain.Common;

namespace Ordering.Domain.Events
{
    public record OrderCreatedEvent : IEvent
    {
        public OrderCreatedEvent(Guid OrderId, Guid UserId, DateTime CreatedAt, Guid AggregateId, int AggregateVersion)
        {
            this.OrderId = OrderId;
            this.UserId = UserId;
            this.CreatedAt = CreatedAt;
            this.AggregateVersion = AggregateVersion;
            this.AggregateId = AggregateId;
        }

        public Guid AggregateId { get; set; }
        public int AggregateVersion { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }

    }
}
