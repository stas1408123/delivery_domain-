using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.ReadModels.Orders.Models;
using Ordering.Application.Services;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Cache;
using Ordering.Infrastructure.EventStore;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure.Serializer;


namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEventStoreSerializer, EventStoreJsonSerializer>();
            builder.Services.AddScoped<IEventStore, EventStoreRepository>();
            builder.Services.AddMemoryCache();

            builder.Services.AddSingleton<ICacheRepository, MemoryCacheRepository>();
            builder.Services.AddSingleton<ISnapshotCache<OrderSnapshot>, SnapshotCache<OrderSnapshot>>();
            builder.Services.AddScoped(typeof(IEventSourcedRepository<Order>), typeof(OrderRepository));
            builder.Services.AddScoped<IOrderReadModelRepository, OrderReadModelRepository>();

            builder.Services.AddScoped(typeof(IEventSourcedRepository<>), typeof(EventSourcedRepository<>));
            builder.Services.AddDbContext<OrderingDbContext>((options) =>
            {
                options.UseInMemoryDatabase("OrderingDB");
            });

            builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<OrderingDbContext>());
        }
    }
}
