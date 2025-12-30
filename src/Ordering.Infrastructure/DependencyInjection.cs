using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure.Serializer;


namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEventStoreSerializer, EventStoreJsonSerializer>();
            builder.Services.AddScoped<IEventStore,EventStoreRepository>();
            builder.Services.AddDbContext<OrderingDbContext>((options) =>
            {
                options.UseInMemoryDatabase("OrderingDB");
            });

            builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<OrderingDbContext>());
        }
    }
}
