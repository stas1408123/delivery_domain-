using Ordering.Domain.Common;

namespace Ordering.Domain.Entities
{
    public class Dish : BaseEntity
    {
        private decimal subTotal;
        private int amount;
        private decimal cost;

        public int Amount
        {
            get => amount;
            set
            {
                amount = value;
                subTotal = this.Amount * this.Cost;
            }
        }

        public decimal Cost 
        { 
            get => cost; 
            set
            {
                cost = value;
                subTotal = this.Amount * this.Cost;
            }
        }

        public Guid OrderId { get; set; }

        public Order order { get; set; }

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
