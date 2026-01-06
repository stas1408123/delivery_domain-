using Ordering.Application.ReadModels.Orders.Models;
using Ordering.Domain.AggregatesModels.OrderAggregate;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record OrderDraftDTO
    {
        public Guid Id { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; init; }
        public decimal Total { get; init; }
        public OrderStatus Status { get; set; }

        public static OrderDraftDTO FromOrder(Order order)
        {
            return new OrderDraftDTO()
            {
                OrderItems = order.Dishes.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    OrderId = oi.OrderId,
                    Amount = oi.Amount,
                    Cost = oi.Cost,
                    SubTotal = oi.SubTotal
                }),
                Id = order.Id,
                Total = order.TotalAmount,
                Status = order.Status,
            };
        }

        public static OrderDraftDTO FromReadModel(OrderReadModel readModel)
        {
            return new OrderDraftDTO
            {
                Id = readModel.Id,
                Total = readModel.TotalAmount,
                Status = readModel.Status,
                OrderItems = readModel.Dishes.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    OrderId = readModel.Id,
                    Amount = oi.Amount,
                    Cost = oi.Cost,
                    SubTotal = oi.Amount * oi.Cost
                })
            };
        }

    }

    public record OrderItemDTO
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Amount { get; init; }

        public decimal Cost { get; init; }

        public decimal SubTotal { get; init; }
    }
}
