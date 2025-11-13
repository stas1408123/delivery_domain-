using Ordering.Domain.Common;

namespace Ordering.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        public int ItemCount { get; set; }

        public IList<Dish> Dishes { get; set; } = new List<Dish>();
    }
}

// -Идентификатор пользователя
// - Общее количество блюд в заказе  
// - Общая стоимость заказа  
