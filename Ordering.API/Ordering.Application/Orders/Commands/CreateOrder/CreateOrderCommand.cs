using MediatR;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(Guid BuyerId, IEnumerable<BasketItem> Items) : IRequest<OrderDraftDTO> { }

    public class BasketItem
    {
        public Guid Id { get; init; }

        public int Amount { get; init; }

        public decimal Cost { get; init; }

        public decimal SubTotal { get; init; }
    }
}
