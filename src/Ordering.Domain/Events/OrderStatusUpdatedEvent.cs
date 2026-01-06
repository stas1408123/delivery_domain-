using MediatR;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Common;

namespace Ordering.Domain.Events
{
    public record OrderStatusUpdatedEvent : IEvent, INotification
    {
        public OrderStatusUpdatedEvent
            (Guid orderId,
            OrderStatus status,
            Guid aggregateId,
            int aggregateVersion,
            DateTime createdAt)
        {
            OrderId = orderId;
            Status = status;
            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;
            CreatedAt = createdAt;
        }

        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public Guid AggregateId { get; set; }
        public int AggregateVersion { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
