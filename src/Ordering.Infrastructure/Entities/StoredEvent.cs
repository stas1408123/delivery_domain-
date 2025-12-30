namespace Ordering.Infrastructure.Entities
{
    public class StoredEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AggregateId { get; set; }
        public string AggregateType { get; set; }
        public int AggregateVersion { get; set; }
        public string EventType { get; set; }
        // JSONB ??? 
        public string Data { get; set; } 
        public DateTimeOffset Timestamp { get; set; }
    }
}
