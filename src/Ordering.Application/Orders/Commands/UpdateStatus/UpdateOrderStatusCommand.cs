using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.AggregatesModels.OrderAggregate;

namespace Ordering.Application.Orders.Commands.UpdateStatus
{
    public record UpdateOrderStatusCommand(Guid OrderId, OrderStatus status) : IRequest<OrderDraftDTO> { }
}
