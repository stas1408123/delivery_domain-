using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.ReadModels.Orders.Models;
using Ordering.Domain.AggregatesModels.OrderAggregate;
using Ordering.Domain.Events;

namespace Ordering.Application.ReadModels.Orders.Projections
{
    public class OrderProjection(IOrderReadModelRepository repository) :
        INotificationHandler<DishAddedToOrderEvent>,
        INotificationHandler<DishUpdatedInOrderEvent>,
        INotificationHandler<DishDeletedFromOrderEvent>,
        INotificationHandler<OrderCreatedEvent>,
        INotificationHandler<OrderStatusUpdatedEvent>
    {
        public async Task Handle(OrderCreatedEvent e, CancellationToken ct)
        {
            var order = new OrderReadModel
            {
                Id = e.OrderId,
                UserId = e.UserId,
                Status = OrderStatus.New,
                TotalAmount = 0,
                ItemCount = 0
            };

            await repository.Add(order, ct);
        }

        public async Task Handle(OrderStatusUpdatedEvent e, CancellationToken ct)
        {
            var order = await repository.Get(e.OrderId, ct);

            order.Status = e.Status;
            await repository.Update(order, ct);
        }

        public async Task Handle(DishAddedToOrderEvent e, CancellationToken ct)
        {
            var order = await repository.Get(e.OrderId, ct);

            var dish = order.Dishes.FirstOrDefault(d => d.ProductId == e.ProductId);
            if (dish == null)
            {
                order.Dishes.Add(new OrderDishReadModel
                {
                    ProductId = e.ProductId,
                    Amount = e.Amount,
                    Cost = e.Cost
                });
            }
            else
            {
                dish.Amount += e.Amount;
            }

            Recalculate(order);
            await repository.Update(order, ct);
        }

        public async Task Handle(DishUpdatedInOrderEvent e, CancellationToken ct)
        {
            var order = await repository.Get(e.OrderId, ct);

            var dish = order.Dishes.FirstOrDefault(d => d.ProductId == e.ProductId);
            if (dish is null) return;

            dish.Amount = e.Amount;
            dish.Cost = e.Cost;

            Recalculate(order);
            await repository.Update(order, ct);
        }

        public async Task Handle(DishDeletedFromOrderEvent e, CancellationToken ct)
        {
            var order = await repository.Get(e.OrderId, ct);

            order.Dishes.RemoveAll(d => d.ProductId == e.ProductId);

            Recalculate(order);
            await repository.Update(order, ct);
        }

        private static void Recalculate(OrderReadModel order)
        {
            order.ItemCount = order.Dishes.Count;
            order.TotalAmount = order.Dishes.Sum(d => d.Amount * d.Cost);
        }
    }
}

