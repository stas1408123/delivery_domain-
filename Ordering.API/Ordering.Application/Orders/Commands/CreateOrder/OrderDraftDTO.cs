using Ordering.Domain.Entities;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record OrderDraftDTO
    {
        public Guid Id { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; init; }
        public decimal Total { get; init; }

        public static OrderDraftDTO FromOrder(Order order)
        {
            return new OrderDraftDTO()
            {
                OrderItems = order.Dishes.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    OrderId = oi.OrderId,
                    Amount = oi.Amount,
                    Cost = oi.Cost,
                    SubTotal = oi.SubTotal
                }),
                Id = order.Id,
                Total = order.TotalAmount,
            };
        }
    }

    public record OrderItemDTO
    {
        public Guid Id { get; init; }

        public Guid OrderId { get; set; }

        public int Amount { get; init; }

        public decimal Cost { get; init; }

        public decimal SubTotal { get; init; }
    }
}
