using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Services;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Events;

namespace Ordering.Application.Dishes.Commands
{
    public class UpdateDishCommandHandler(IEventSourcedRepository<Order> _orderRepository, IMediator _mediator) : IRequestHandler<UpdateDishCommand, OrderDraftDTO>
    {
        public async Task<OrderDraftDTO> Handle(UpdateDishCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            var dishUpdatedInOrderEvent = new DishUpdatedInOrderEvent(
                command.OrderId,
                command.Item.ProductId,
                command.Item.Cost,
                command.Item.Amount,
                command.OrderId,
                order.Version);

            order.AppendEvent(dishUpdatedInOrderEvent);

            await _orderRepository.SaveAsync(order);
            await _mediator.Publish(dishUpdatedInOrderEvent);

            return OrderDraftDTO.FromOrder(order);
        }
    }
}
