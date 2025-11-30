using Ordering.Domain.Common;

namespace Ordering.Domain.AggregatesModels.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }

        public decimal TotalAmount { get; private set; }

        public OrderStatus Status { get; private set; }

        public int ItemCount { get; set; }

        public IList<Dish> Dishes { get; set; } = new List<Dish>();

        public void CalculateTotalAmount()
        {
            if (Dishes.Count == 0)
            {
                this.TotalAmount = 0;
            }

            this.TotalAmount = Dishes.Sum(x => x.SubTotal);
        }

        public void AddDish(Dish dish)
        {
            var dishInOrder = Dishes.SingleOrDefault(d => d.ProductId == d.ProductId);

            if (dishInOrder == null)
            {
                Dishes.Add(dish);
            }
            else
            {
                dishInOrder.Amount += dish.Amount;
            }

            CalculateTotalAmount();
        }

        public void DeleteDish(Dish dish)
        {
            var dishInOrder = Dishes.SingleOrDefault(d => d.ProductId == d.ProductId);

            if (dishInOrder == null)
            {
                return;
            }

            Dishes.Remove(dish);
            CalculateTotalAmount();
        }

        public void UpdateDish(Dish dish)
        {
            var dishInOrder = Dishes.SingleOrDefault(d => d.ProductId == d.ProductId);

            if (dish == null)
            {
                Dishes.Add(dish);
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
