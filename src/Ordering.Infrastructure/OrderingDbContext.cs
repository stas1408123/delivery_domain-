using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.ReadModels.Orders.Models;
using Ordering.Infrastructure.Entities;

namespace Ordering.Infrastructure
{
    public class OrderingDbContext : DbContext, IApplicationDbContext
    {
        public OrderingDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<OrderReadModel> OrderReadModels { get; set; }
        public DbSet<OrderDishReadModel> OrderDishReadModels { get; set; }
        public DbSet<StoredEvent> Events { get; set; }
    }
}
