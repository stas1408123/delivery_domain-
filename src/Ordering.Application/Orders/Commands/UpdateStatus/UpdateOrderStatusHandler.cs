using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Common;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.Commands.UpdateStatus
{
    public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, OrderDraftDTO>
    {
        private readonly IEventStore _eventStore;

        public UpdateOrderStatusHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<OrderDraftDTO> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var events = (await _eventStore.Fetch(command.OrderId)).OrderBy(e => e.AggregateVersion);

            var currentLatestVersion = events.Max(x => x.AggregateVersion);

            var order = new Domain.AggregatesModels.OrderAggregate.Order();

            events.ToList().ForEach(e => order.Apply(e));

            var orderStatusUpdatedEvent = new OrderStatusUpdatedEvent(command.OrderId, command.status, command.OrderId, currentLatestVersion, DateTime.Now);

            order.Apply(orderStatusUpdatedEvent);

            await _eventStore.Append(order.Id, [orderStatusUpdatedEvent], orderStatusUpdatedEvent.AggregateVersion);

            return OrderDraftDTO.FromOrder(order);
        }
    }
}
