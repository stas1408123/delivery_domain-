using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.Application.Common.Interfaces;


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
