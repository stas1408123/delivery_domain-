using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Services;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Events;

namespace Ordering.Application.Dishes.Commands
{
    public class AddDishCommandHandler(IEventSourcedRepository<Order> _orderRepository, IMediator _mediator) : IRequestHandler<AddDishCommand, OrderDraftDTO>
    {
        public async Task<OrderDraftDTO> Handle(AddDishCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            var dishAddedToOrderEvent = new DishAddedToOrderEvent(
                command.OrderId,
                command.Item.ProductId,
                command.Item.Cost,
                command.Item.Amount,
                command.OrderId,
                order.Version);

            order.AppendEvent(dishAddedToOrderEvent);

            await _orderRepository.SaveAsync(order);

            await _mediator.Publish(dishAddedToOrderEvent);

            return OrderDraftDTO.FromOrder(order);
        }
    }
}
