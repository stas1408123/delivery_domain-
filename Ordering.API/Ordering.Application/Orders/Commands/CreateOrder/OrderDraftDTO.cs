using Ordering.Domain.Entities;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record OrderDraftDTO
    {
        public IEnumerable<OrderItemDTO> OrderItems { get; init; }
        public decimal Total { get; init; }

        public static OrderDraftDTO FromOrder(Order order)
        {
            return new OrderDraftDTO()
            {
                OrderItems = order.Dishes.Select(oi => new OrderItemDTO
                {
                    Amount = oi.Amount,
                    Cost = oi.Cost,
                    SubTotal = oi.SubTotal
                }),
                Total = order.TotalAmount,
            };
        }
    }

    public record OrderItemDTO
    {
        public int Amount { get; init; }

        public decimal Cost { get; init; }

        public decimal SubTotal { get; init; }
    }
}
