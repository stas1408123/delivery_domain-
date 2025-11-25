using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;

namespace Ordering.Application.Orders.Commands.CreateOrder;



public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDraftDTO>
{
    private readonly IApplicationDbContext _context;

    public CreateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDraftDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = new Order();

        entity.UserId = request.BuyerId;

        entity.Status = Domain.OrderStatus.New;

        foreach (var item in request.Items)
        {
            entity.Dishes.Add(new Dish(item.Amount, item.Cost));
        }

        entity.CalculateTotalAmount();

        _context.Orders.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return OrderDraftDTO.FromOrder(entity);
    }
}

