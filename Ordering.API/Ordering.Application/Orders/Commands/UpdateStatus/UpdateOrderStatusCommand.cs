using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain;

namespace Ordering.Application.Orders.Commands.UpdateStatus
{
    public record UpdateOrderStatusCommand(Guid OrderId, OrderStatus status) : IRequest<OrderDraftDTO> { }
}
