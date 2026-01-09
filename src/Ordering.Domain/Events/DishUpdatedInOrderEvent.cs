using MediatR;
using Ordering.Domain.Common;

namespace Ordering.Domain.Events
{
    public record DishUpdatedInOrderEvent : IEvent, INotification
    {
        public DishUpdatedInOrderEvent(
            Guid orderId,
            Guid productId,
            decimal cost,
            int amount,
            Guid aggregateId,
            int aggregateVersion)
        {
            OrderId = orderId;
            ProductId = productId;
            Cost = cost;
            Amount = amount;
            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;
        }

        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
        public decimal Cost { get; init; }
        public int Amount { get; init; }
        public Guid AggregateId { get; set; }
        public int AggregateVersion { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
