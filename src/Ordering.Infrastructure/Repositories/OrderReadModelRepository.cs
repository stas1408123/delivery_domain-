using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.ReadModels.Orders.Models;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderReadModelRepository : IOrderReadModelRepository
    {
        private readonly OrderingDbContext _context;

        public OrderReadModelRepository(OrderingDbContext context)
        {
            _context = context;
        }

        public async Task Add(OrderReadModel entity, CancellationToken cancellationToken)
        {
            await _context.OrderReadModels.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<OrderReadModel> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.OrderReadModels
                .Include(o => o.Dishes)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public virtual IQueryable<OrderReadModel> Get(Expression<Func<OrderReadModel, bool>> predicate)
        {
            return _context.OrderReadModels.Where(predicate).AsNoTracking();
        }

        public async Task Update(OrderReadModel entity, CancellationToken cancellationToken)
        {
            _context.OrderReadModels.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
