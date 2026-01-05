namespace Ordering.Domain.Common
{
    public interface IEvent
    {
        Guid AggregateId { get; set; }
        int AggregateVersion { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
