using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Common;

namespace Ordering.Application.Orders.Queries.GetOrder
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDraftDTO>>
    {
        private readonly IEventStore _eventStore;

        public GetOrdersQueryHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        // ToDo add common select order with optional id 
        public async Task<IEnumerable<OrderDraftDTO>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var order = new Order();

            var events = await _eventStore.Fetch(request.Id);

            foreach (var e in events)
            {
                order.Apply(e);
            }

            return (new List<Order>([order])).Select(OrderDraftDTO.FromOrder);
        }
    }
}
