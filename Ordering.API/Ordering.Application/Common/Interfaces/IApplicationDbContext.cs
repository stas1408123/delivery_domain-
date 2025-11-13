using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; }

        DbSet<Dish> Dishs { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
