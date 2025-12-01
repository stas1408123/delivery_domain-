using Microsoft.EntityFrameworkCore;
using Ordering.Domain.AggregatesModels.OrderAggregate;

namespace Ordering.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; }

        DbSet<Dish> Dishes { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
