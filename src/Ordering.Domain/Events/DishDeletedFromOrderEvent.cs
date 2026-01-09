using MediatR;
using Ordering.Domain.Common;

namespace Ordering.Domain.Events
{
    public record DishDeletedFromOrderEvent : IEvent, INotification
    {
        public DishDeletedFromOrderEvent(Guid orderId, Guid productId, Guid aggregateId, int aggregateVersion)
        {
            OrderId = orderId;
            ProductId = productId;
            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;
        }

        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
        public Guid AggregateId { get; set; }
        public int AggregateVersion { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
