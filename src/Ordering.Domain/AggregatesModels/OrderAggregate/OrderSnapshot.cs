using Ordering.Domain.Common;

namespace Ordering.Domain.AggregatesModels.OrderAggregate
{
    public class OrderSnapshot : BaseSnapshot
    {
        public Guid UserId { get; init; }
        public OrderStatus Status { get; init; }
        public IReadOnlyList<DishSnapshot> Dishes { get; init; }

        public OrderSnapshot(
            Guid id,
            int version,
            Guid userId,
            decimal totalAmount,
            OrderStatus status,
            List<DishSnapshot> dishes) : base(id, version)
        {
            UserId = userId;
            Status = status;
            Dishes = dishes.AsReadOnly();
        }
    }

}
