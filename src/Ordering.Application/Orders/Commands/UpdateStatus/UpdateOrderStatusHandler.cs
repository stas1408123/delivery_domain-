using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Services;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.Commands.UpdateStatus
{
    public class UpdateOrderStatusHandler(IEventSourcedRepository<Order> _orderRepository) : IRequestHandler<UpdateOrderStatusCommand, OrderDraftDTO>
    {
        public async Task<OrderDraftDTO> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            var orderStatusUpdatedEvent = new OrderStatusUpdatedEvent(command.OrderId, command.status, command.OrderId, order.Version, DateTime.Now);

            order.AppendEvent(orderStatusUpdatedEvent);

            await _orderRepository.SaveAsync(order);

            return OrderDraftDTO.FromOrder(order);
        }
    }
}
