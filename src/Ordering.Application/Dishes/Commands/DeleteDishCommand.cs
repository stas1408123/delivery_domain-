using MediatR;

namespace Ordering.Application.Dishes.Commands
{
    public record DeleteDishCommand(Guid OrderId, Guid ProductId) : IRequest { }
}
