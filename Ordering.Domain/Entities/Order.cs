using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid TotalAmount { get; set; }

        public OrderStatus status { get; set; }

        public IList<Dish> dishes { get; set; } = new List<Dish>();
    }
}

// #### Заказ:  
// -Идентификатор пользователя
// - Общее количество блюд в заказе  
// - Общая стоимость заказа  
