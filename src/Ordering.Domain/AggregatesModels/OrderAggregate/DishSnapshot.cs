namespace Ordering.Domain.AggregatesModels.OrderAggregate
{
    public record DishSnapshot(Guid ProductId, int Amount, decimal Cost);
}
