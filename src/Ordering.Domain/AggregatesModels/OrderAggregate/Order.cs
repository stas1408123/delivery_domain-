using Ordering.Domain.Common;

namespace Ordering.Domain.AggregatesModels.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }

        public decimal TotalAmount { get; private set; }

        public OrderStatus Status { get; private set; }

        public int ItemCount { get; private set; }

        private readonly IList<Dish> dishes = new List<Dish>();

        public IReadOnlyCollection<Dish> Dishes => dishes.AsReadOnly();

        public void CalculateTotalAmount()
        {
            if (dishes.Count == 0)
            {
                this.TotalAmount = 0;
            }

            this.TotalAmount = dishes.Sum(x => x.SubTotal);
        }

        public void AddDish(Dish dish)
        {
            var dishInOrder = dishes.SingleOrDefault(d => d.ProductId == dish.ProductId);

            if (dishInOrder == null)
            {
                dishes.Add(dish);
            }
            else
            {
                dishInOrder.Amount += dish.Amount;
            }

            CalculateTotalAmount();
        }

        public void DeleteDish(Dish dish)
        {
            var dishInOrder = Dishes.SingleOrDefault(d => d.ProductId == dish.ProductId);

            if (dishInOrder == null)
            {
                return;
            }

            dishes.Remove(dish);
            CalculateTotalAmount();
        }

        public void UpdateDish(Dish dish)
        {
            var dishInOrder = Dishes.SingleOrDefault(d => d.ProductId == dish.ProductId);

            if (dish == null)
            {
                dishes.Add(dish);
            }
            else
            {
                dishInOrder.Amount = dish.Amount;
                dishInOrder.Cost = dish.Cost;
            }
            CalculateTotalAmount();
        }

        // ToDo Some business logic for status change 
        public void UpdateStatus(OrderStatus status)
        {
            this.Status = status;
        }

    }
}
