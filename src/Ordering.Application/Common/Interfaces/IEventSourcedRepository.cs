using Ordering.Domain.Common;

namespace Ordering.Application.Services
{
    public interface IEventSourcedRepository<T> where T : class, IEventSourcedAggregate
    {
        Task SaveAsync(T aggregate);
        Task<T> GetByIdAsync(Guid id);
    }
}
