namespace Ordering.Domain.Common
{
    public abstract class BaseSnapshot : ISnapshot
    {
        public Guid AggregateId { get; protected set; }
        public int Version { get; protected set; }

        protected BaseSnapshot(Guid id, int version)
        {
            AggregateId = id;
            Version = version;
        }
    }

}
