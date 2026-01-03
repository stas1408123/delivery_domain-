using Ordering.Domain.Common;

namespace Ordering.Domain.AggregatesModels.OrderAggregate
{
    public class Dish
    {
        private decimal subTotal;
        private int amount;
        private decimal cost;

        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }

        public int Amount
        {
            get => amount;
            set
            {
                amount = value;
                subTotal = Amount * Cost;
            }
        }

        public decimal Cost
        {
            get => cost;
            set
            {
                cost = value;
                subTotal = Amount * Cost;
            }
        }

        public decimal SubTotal
        {
            get => subTotal;
            private set
            {
                subTotal = Amount * Cost;
            }
        }

        public Dish(Guid productId,Guid orderId, int amount, decimal cost)
        {
            ProductId = productId;
            OrderId = orderId;
            Amount = amount;
            Cost = cost;
            SubTotal = Amount * Cost;
        }
    }
}
