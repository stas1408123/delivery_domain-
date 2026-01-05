using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Services;
using Ordering.Domain.AggregatesModels.OrderAggregate;

namespace Ordering.Application.Orders.Queries.GetOrder
{
    public class GetOrdersQueryHandler(IEventSourcedRepository<Order> _orderRepository) : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDraftDTO>>
    {
        // ToDo add common select order with optional id 
        public async Task<IEnumerable<OrderDraftDTO>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            return (new List<Order>([order])).Select(OrderDraftDTO.FromOrder);
        }
    }
}
