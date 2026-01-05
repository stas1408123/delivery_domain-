using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Infrastructure.Entities;

namespace Ordering.Infrastructure
{
    public class OrderingDbContext : DbContext, IApplicationDbContext
    {
        public OrderingDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<StoredEvent> Events { get; set; }
    }
}
