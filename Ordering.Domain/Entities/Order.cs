using Ordering.Domain.Common;

namespace Ordering.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

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
    }
}
