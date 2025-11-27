using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Dishes.Commands
{
    public record UpdateDishCommand(Guid OrderId, BasketItem item) : IRequest<OrderDraftDTO> { }
}
