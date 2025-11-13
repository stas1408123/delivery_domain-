using Ordering.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<OrderingDbContext>((options) =>
            {
                options.UseInMemoryDatabase("OrderingDB");
            });

            builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<OrderingDbContext>());
        }
    }
}
