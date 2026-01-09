using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.ReadModels.Orders.Models;

namespace Ordering.Application.Orders.Queries.GetOrder
{
    public class GetOrdersQueryHandler(IOrderReadModelRepository repository) : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDraftDTO>>
    {
        public async Task<IEnumerable<OrderDraftDTO>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            IQueryable<OrderReadModel> query = repository.Get(o => true)
                                                   .Include(o => o.Dishes);

            if (request.Id.HasValue)
            {
                query = query.Where(o => o.Id == request.Id.Value);
            }

            var orders = await query.AsNoTracking().ToListAsync(cancellationToken);

            return orders.Select(OrderDraftDTO.FromReadModel);
        }
    }
}

