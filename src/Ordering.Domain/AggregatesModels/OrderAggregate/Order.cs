using Ordering.Domain.Common;
using Ordering.Domain.Events;

namespace Ordering.Domain.AggregatesModels.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public Guid UserId { get; set; }

        public decimal TotalAmount { get; private set; }

        public OrderStatus Status { get; private set; }

        public int ItemCount { get; private set; }

        private readonly IList<Dish> dishes = new List<Dish>();

        public IReadOnlyCollection<Dish> Dishes => dishes.AsReadOnly();

        public int Version { get; private set; } = 0;

        private void CalculateTotalAmount()
        {
            if (dishes.Count == 0)
            {
                this.TotalAmount = 0;
            }

            this.TotalAmount = dishes.Sum(x => x.SubTotal);
        }

        private void DishAddedToOrder(DishAddedToOrderEvent e)
        {
            var dishInOrder = dishes.FirstOrDefault(d => d.ProductId == e.ProductId);

            if (dishInOrder == null)
            {
                dishes.Add(new Dish(e.ProductId, e.Amount, e.Cost));
            }
            else
            {
                dishInOrder.Amount += e.Amount;
            }

            CalculateTotalAmount();
        }
        private void DishDeletedFromOrder(DishDeletedFromOrderEvent e)
        {
            var dishInOrder = Dishes.FirstOrDefault(d => d.ProductId == e.ProductId);

            if (dishInOrder == null)
            {
                return;
            }

            dishes.Remove(dishInOrder);
            CalculateTotalAmount();
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
            CalculateTotalAmount();
        }

        public void Apply(IEvent @event)
        {
            ((dynamic)this).Apply((dynamic)@event);
        }

        private void Apply(OrderCreatedEvent e)
        {
            this.Id = e.OrderId;
            this.UserId = e.UserId;
            this.Status = OrderStatus.New;
            Version += 1;
        }

        private void Apply(OrderStatusUpdatedEvent e)
        {
            this.Status = e.Status;
            Version += 1;
        }

        private void Apply(DishAddedToOrderEvent e)
        {
            DishAddedToOrder(e);
            Version += 1;
        }

        private void Apply(DishDeletedFromOrderEvent e)
        {
            DishDeletedFromOrder(e);
            Version += 1;
        }

        private void Apply(DishUpdatedInOrderEvent e)
        {
            DishUpdatedInOrder(e);
            Version += 1;
        }
    }
}
