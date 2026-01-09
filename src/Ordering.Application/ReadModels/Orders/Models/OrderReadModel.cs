using Ordering.Domain.AggregatesModels.OrderAggregate;

namespace Ordering.Application.ReadModels.Orders.Models
{
    public class OrderReadModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }
        public int ItemCount { get; set; }

        public List<OrderDishReadModel> Dishes { get; set; } = new();
    }
}
