using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;

namespace Ordering.Application.Dishes.Commands
{
    public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDishCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteDishCommand command, CancellationToken cancellationToken)
        {
            var dish = await _context.Dishes.SingleAsync(x => x.Id == command.Id);

            var order = await _context.Orders
                .Include(x => x.Dishes)
                .FirstOrDefaultAsync(o => o.Id == dish.OrderId, cancellationToken);

            order.Dishes.Remove(dish);

            order.CalculateTotalAmount();

            _context.Orders.Update(order);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
