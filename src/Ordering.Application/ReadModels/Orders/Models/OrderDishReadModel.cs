using System.ComponentModel.DataAnnotations;

namespace Ordering.Application.ReadModels.Orders.Models
{
    public class OrderDishReadModel
    {
        public Guid Id { get; set; }

        public Guid OrderReadId { get; set; }
        public OrderReadModel? OrderReadModel { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Cost { get; set; }
    }
}
