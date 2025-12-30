using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Common;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.Commands.CreateOrder;



public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDraftDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IEventStore _eventStore;

    public CreateOrderCommandHandler(IApplicationDbContext context, IEventStore eventStore)
    {
        _context = context;
        _eventStore = eventStore;
    }

    public async Task<OrderDraftDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order();
        var orderId = Guid.NewGuid();

        //foreach (var item in request.Items)
        //{
        //    entity.AddDish(new Dish(item.ProductId, item.Amount, item.Cost));
        //}

        var orderCreatedEvent = new OrderCreatedEvent(orderId, request.BuyerId, DateTime.Now, orderId, 0);
        await _eventStore.Append(orderCreatedEvent.OrderId, [orderCreatedEvent], 1);

        order.Apply(orderCreatedEvent);


        return OrderDraftDTO.FromOrder(order);
    }
}

