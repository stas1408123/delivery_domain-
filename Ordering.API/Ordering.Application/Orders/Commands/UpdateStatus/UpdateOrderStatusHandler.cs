using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.Commands.UpdateStatus
{
    public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, OrderDraftDTO>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOrderStatusHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDraftDTO> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var order = _context.Orders.Single(x => x.Id == command.OrderId);

            // ToDo Some business logic for status change 
            order.Status = command.status;

            await _context.SaveChangesAsync(cancellationToken);

            return OrderDraftDTO.FromOrder(order);
        }
    }
}
