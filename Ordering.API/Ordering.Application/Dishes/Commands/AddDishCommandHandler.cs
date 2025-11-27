using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Entities;

namespace Ordering.Application.Dishes.Commands
{
    public class AddDishCommandHandler : IRequestHandler<AddDishCommand, OrderDraftDTO>
    {
        private readonly IApplicationDbContext _context;

        public AddDishCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDraftDTO> Handle(AddDishCommand command, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(x => x.Dishes)
                .FirstOrDefaultAsync(o => o.UserId == command.BuyerId, cancellationToken);

            var dish = new Dish(command.Item.Amount, command.Item.Cost);

            order.Dishes.Add(dish);

            order.CalculateTotalAmount();

            _context.Orders.Update(order);

            await _context.SaveChangesAsync(cancellationToken);

            return OrderDraftDTO.FromOrder(order);
        }
    }
}
