using Ordering.Domain.Common;

namespace Ordering.Domain.Entities
{
    public class Dish : BaseEntity
    {
        public int Amount { get; set; }

        public decimal Cost { get; set; }

        public decimal SubTotal { get; set; }
    }
}

//#### Блюдо в заказе:  
//-Идентификатор блюда
//- Количество
//- Цена за единицу  
//- Сумма (цена × количество)  
