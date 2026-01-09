using Ordering.Application.ReadModels.Orders.Models;
using System.Linq.Expressions;

namespace Ordering.Application.Common.Interfaces
{
    public interface IOrderReadModelRepository
    {
        Task Add(OrderReadModel entity, CancellationToken cancellationToken);
        Task<OrderReadModel> Get(Guid id, CancellationToken cancellationToken);
        Task Update(OrderReadModel entity, CancellationToken cancellationToken);
        IQueryable<OrderReadModel> Get(Expression<Func<OrderReadModel, bool>> predicate);
    }
}
