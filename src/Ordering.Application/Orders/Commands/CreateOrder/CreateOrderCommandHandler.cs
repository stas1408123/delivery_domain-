using MediatR;
using Ordering.Application.Services;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IEventSourcedRepository<Order> _orderRepository, IMediator _mediator) : IRequestHandler<CreateOrderCommand, OrderDraftDTO>
{
    public async Task<OrderDraftDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order();
        var orderId = Guid.NewGuid();

        var orderCreatedEvent = new OrderCreatedEvent(orderId, request.BuyerId, DateTime.Now, orderId, 0);
        order.AppendEvent(orderCreatedEvent);

        foreach (var item in request.Items)
        {
            order.AppendEvent(new DishAddedToOrderEvent(
                orderId,
                item.ProductId,
                item.Cost,
                item.Amount,
                orderId,
                order.Version));
        }
        var events = order.GetUncommittedEvents().ToList();
        await _orderRepository.SaveAsync(order);

        foreach (var @event in events)
        {
            await _mediator.Publish(@event);
        }

        return OrderDraftDTO.FromOrder(order);
    }
}

