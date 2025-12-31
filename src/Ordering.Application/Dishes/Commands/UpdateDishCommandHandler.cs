using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Common;
using Ordering.Domain.Events;

namespace Ordering.Application.Dishes.Commands
{
    public class UpdateDishCommandHandler(IEventStore eventStore) : IRequestHandler<UpdateDishCommand, OrderDraftDTO>
    {
        public async Task<OrderDraftDTO> Handle(UpdateDishCommand command, CancellationToken cancellationToken)
        {
            var events = (await eventStore.Fetch(command.OrderId)).OrderBy(e => e.AggregateVersion);

            var currentLatestVersion = events.Max(x => x.AggregateVersion);

            var order = new Order();

            events.ToList().ForEach(e => order.Apply(e));

            var dishUpdatedInOrderEvent = new DishUpdatedInOrderEvent(
                command.OrderId,
                command.Item.ProductId,
                command.Item.Cost,
                command.Item.Amount,
                command.OrderId,
                currentLatestVersion);

            order.Apply(dishUpdatedInOrderEvent);

            await eventStore.Append(order.Id, [dishUpdatedInOrderEvent], dishUpdatedInOrderEvent.AggregateVersion);

            return OrderDraftDTO.FromOrder(order);
        }
    }
}
