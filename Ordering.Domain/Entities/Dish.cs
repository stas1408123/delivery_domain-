using Ordering.Domain.Common;

namespace Ordering.Domain.Entities
{
    public class Dish : BaseEntity
    {
        private decimal subTotal;

        public int Amount { get; set; }

        public decimal Cost { get; set; }
        public decimal SubTotal
        {
            get => this.subTotal;
            private set
            {
                subTotal = this.Amount * this.Cost;
            }
        }

        public Dish(int amount, decimal cost)
        {
            Amount = amount;
            Cost = cost;
            SubTotal = this.Amount * this.Cost;
        }
    }
}
