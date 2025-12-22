using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.AggregatesModels.OrderAggregate;

namespace Ordering.Application.Dishes.Commands
{
    public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, OrderDraftDTO>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDishCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDraftDTO> Handle(UpdateDishCommand command, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(x => x.Dishes)
                .FirstOrDefaultAsync(o => o.Id == command.OrderId, cancellationToken);

            var dish = new Dish(command.item.ProductId, command.item.Amount, command.item.Cost);

            order.UpdateDish(dish);

            await _context.SaveChangesAsync(cancellationToken);

            return OrderDraftDTO.FromOrder(order);
        }
    }
}
