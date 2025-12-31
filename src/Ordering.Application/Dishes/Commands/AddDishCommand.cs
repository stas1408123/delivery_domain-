using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Dishes.Commands
{
    public record AddDishCommand(Guid OrderId, BasketItem Item) : IRequest<OrderDraftDTO> { }
}