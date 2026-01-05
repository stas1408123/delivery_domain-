using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Services;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Events;

namespace Ordering.Application.Dishes.Commands
{
    public class AddDishCommandHandler(IEventSourcedRepository<Order> _orderRepository) : IRequestHandler<AddDishCommand, OrderDraftDTO>
    {
        public async Task<OrderDraftDTO> Handle(AddDishCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            order.AppendEvent(new DishAddedToOrderEvent(
                command.OrderId,
                command.Item.ProductId,
                command.Item.Cost,
                command.Item.Amount,
                command.OrderId,
                order.Version));

            await _orderRepository.SaveAsync(order);

            return OrderDraftDTO.FromOrder(order);
        }
    }
}
