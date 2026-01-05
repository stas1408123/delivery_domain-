using MediatR;
using Ordering.Application.Services;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Events;

namespace Ordering.Application.Dishes.Commands
{
    public class DeleteDishCommandHandler(IEventSourcedRepository<Order> _orderRepository) : IRequestHandler<DeleteDishCommand>
    {
        public async Task Handle(DeleteDishCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            var dishDeletedFromOrderEvent = new DishDeletedFromOrderEvent(
                command.OrderId,
                command.ProductId,
                command.OrderId,
                order.Version);

            order.AppendEvent(dishDeletedFromOrderEvent);
            await _orderRepository.SaveAsync(order);

        }
    }
}
