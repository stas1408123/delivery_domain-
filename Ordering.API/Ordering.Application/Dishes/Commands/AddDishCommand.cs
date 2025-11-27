using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Dishes.Commands
{
    public record AddDishCommand(Guid BuyerId, BasketItem Item) : IRequest<OrderDraftDTO> { }
}