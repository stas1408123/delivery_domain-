using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.AggregatesModels.OrderAggregate;

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

        entity.UpdateStatus(OrderStatus.New);

        foreach (var item in request.Items)
        {
            entity.AddDish(new Dish(item.ProductId, item.Amount, item.Cost));
        }

        _context.Orders.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return OrderDraftDTO.FromOrder(entity);
    }
}

