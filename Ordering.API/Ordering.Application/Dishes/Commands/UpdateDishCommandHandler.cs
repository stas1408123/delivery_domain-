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

            ChangeDishInfo(command, order.Dishes.Single(x => x.Id == command.item.Id));

            order.CalculateTotalAmount();

            _context.Orders.Update(order);

            await _context.SaveChangesAsync(cancellationToken);

            return OrderDraftDTO.FromOrder(order);
        }

        private static void ChangeDishInfo(UpdateDishCommand command, Dish dish)
        {
            dish.Cost = command.item.Cost;
            dish.Amount = command.item.Amount;
        }
    }
}
