using Ordering.Domain.Common;

namespace Ordering.Domain.Entities
{
    public class Dish : BaseEntity
    {
        public Dish(int amount, decimal cost)
        {
            Amount = amount;
            Cost = cost;
            SubTotal = this.Amount * this.Cost;
        }

        private decimal subTotal;

        public int Amount { get; set; }

        public decimal Cost { get; set; }

        public decimal SubTotal
        {
            get => this.subTotal;
            set
            {
                subTotal = this.Amount * this.Cost;
            }
        }
    }
}

//#### Блюдо в заказе:  
//-Идентификатор блюда
//- Количество
//- Цена за единицу  
//- Сумма (цена × количество)  
