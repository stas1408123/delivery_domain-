using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Common;
using Ordering.Domain.Events;

namespace Ordering.Domain.AggregatesModels.OrderAggregate
{
    public class Order : BaseEventSourcedAggregate, IAggregateRoot, ISnapshotable<OrderSnapshot>
    {
        public Guid UserId { get; private set; }

        public decimal TotalAmount { get; private set; }

        public OrderStatus Status { get; private set; }

        public int ItemCount { get; private set; } = 0;

        private readonly IList<Dish> dishes = new List<Dish>();

        public IReadOnlyCollection<Dish> Dishes => dishes.AsReadOnly();

        private void CalculateTotalAmount()
        {
            if (dishes.Count == 0)
            {
                this.TotalAmount = 0;
                return;
            }

            this.TotalAmount = dishes.Sum(x => x.SubTotal);
        }

        private void DishAddedToOrder(DishAddedToOrderEvent e)
        {
            var dishInOrder = dishes.FirstOrDefault(d => d.ProductId == e.ProductId);

            if (dishInOrder == null)
            {
                dishes.Add(new Dish(e.ProductId, e.OrderId, e.Amount, e.Cost));
            }
            else
            {
                dishInOrder.Amount += e.Amount;
            }

            RecalculateDerivedState();
        }
        private void DishDeletedFromOrder(DishDeletedFromOrderEvent e)
        {
            var dishInOrder = Dishes.FirstOrDefault(d => d.ProductId == e.ProductId);

            if (dishInOrder == null)
            {
                return;
            }

            dishes.Remove(dishInOrder);
            RecalculateDerivedState();
        }

        private void DishUpdatedInOrder(DishUpdatedInOrderEvent e)
        {
            var dishInOrder = Dishes.FirstOrDefault(d => d.ProductId == e.ProductId);

            if (dishInOrder == null)
            {
                return;
            }
            else
            {
                dishInOrder.Amount = e.Amount;
                dishInOrder.Cost = e.Cost;
            }
            RecalculateDerivedState();
        }

        public override void Apply(IEvent @event)
        {
            ((dynamic)this).Apply((dynamic)@event);
        }

        private void Apply(OrderCreatedEvent e)
        {
            this.Id = e.OrderId;
            this.UserId = e.UserId;
            this.Status = OrderStatus.New;
        }

        private void Apply(OrderStatusUpdatedEvent e)
        {
            this.Status = e.Status;
        }

        private void Apply(DishAddedToOrderEvent e)
        {
            DishAddedToOrder(e);
        }

        private void Apply(DishDeletedFromOrderEvent e)
        {
            DishDeletedFromOrder(e);
        }

        private void Apply(DishUpdatedInOrderEvent e)
        {
            DishUpdatedInOrder(e);
        }

        private void RecalculateDerivedState()
        {
            ItemCount = dishes.Count;
            TotalAmount = dishes.Sum(d => d.SubTotal);
        }

        public OrderSnapshot CreateSnapshot()
        {
            var dishSnapshots = this.dishes
                .Select(d => new DishSnapshot(
                    d.ProductId,
                    d.Amount,
                    d.Cost))
                .ToList();

            return new OrderSnapshot(
                id: this.Id,
                version: this.Version,
                userId: this.UserId,
                totalAmount: this.TotalAmount,
                status: this.Status,
                dishes: dishSnapshots
            );
        }

        public void RestoreFromSnapshot(OrderSnapshot snapshot)
        {
            Id = snapshot.AggregateId;
            Version = snapshot.Version;
            UserId = snapshot.UserId;
            Status = snapshot.Status;

            dishes.Clear();
            foreach (var dish in snapshot.Dishes)
            {
                dishes.Add(new Dish(
                    productId: dish.ProductId,
                    orderId: Id,
                    amount: dish.Amount,
                    cost: dish.Cost
                ));
            }

            RecalculateDerivedState();
        }

    }
}
