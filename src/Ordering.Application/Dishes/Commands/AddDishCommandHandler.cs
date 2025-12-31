using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Common;
using Ordering.Domain.Events;

namespace Ordering.Application.Dishes.Commands
{
    public class AddDishCommandHandler(IEventStore eventStore) : IRequestHandler<AddDishCommand, OrderDraftDTO>
    {
        public async Task<OrderDraftDTO> Handle(AddDishCommand command, CancellationToken cancellationToken)
        {
            var events = (await eventStore.Fetch(command.OrderId)).OrderBy(e => e.AggregateVersion);

            var currentLatestVersion = events.Max(x => x.AggregateVersion);

            var order = new Order();

            events.ToList().ForEach(e => order.Apply(e));

            var dishAddedToOrderEvent = new DishAddedToOrderEvent(
                command.OrderId, 
                command.Item.ProductId, 
                command.Item.Cost, 
                command.Item.Amount,
                command.OrderId, 
                currentLatestVersion);

            order.Apply(dishAddedToOrderEvent);

            await eventStore.Append(order.Id, [dishAddedToOrderEvent], dishAddedToOrderEvent.AggregateVersion);

            return OrderDraftDTO.FromOrder(order);
        }
    }
}
