using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.Queries.GetOrder
{
    public record GetOrdersQuery(Guid? Id) : IRequest<IEnumerable<OrderDraftDTO>>
    {
    }
}
