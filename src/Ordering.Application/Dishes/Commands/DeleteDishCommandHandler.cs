using MediatR;
using Ordering.Domain.Common;
using Ordering.Domain.Events;

namespace Ordering.Application.Dishes.Commands
{
    public class DeleteDishCommandHandler(IEventStore eventStore) : IRequestHandler<DeleteDishCommand>
    {
        public async Task Handle(DeleteDishCommand command, CancellationToken cancellationToken)
        {
            var events = (await eventStore.Fetch(command.OrderId)).OrderBy(e => e.AggregateVersion);

            var currentLatestVersion = events.Max(x => x.AggregateVersion);

            var dishAddedToOrderEvent = new DishDeletedFromOrderEvent(
                command.OrderId,
                command.ProductId,
                command.OrderId,
                currentLatestVersion);

            await eventStore.Append(command.OrderId, [dishAddedToOrderEvent], dishAddedToOrderEvent.AggregateVersion);
        }
    }
}
