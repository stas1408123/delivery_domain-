using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure
{
    public class OrderingDbContext : DbContext, IApplicationDbContext
    {
        public OrderingDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Dish> Dishes { get; set; }
    }
}
