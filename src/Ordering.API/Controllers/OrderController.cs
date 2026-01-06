using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Orders.Commands.UpdateStatus;
using Ordering.Application.Orders.Queries.GetOrder;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDraftDTO>> GetOrders(Guid? Id)
        {
            var command = new GetOrdersQuery(Id);

            _logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                "Get Orders",
                nameof(command),
                "",
                command);

            return await _mediator.Send(command);
        }



        [HttpPost]
        public async Task<OrderDraftDTO> CreateOrderDraftAsync(CreateOrderCommand command)
        {
            _logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                "Draft Order",
                nameof(command.BuyerId),
                command.BuyerId,
                command);

            return await _mediator.Send(command);
        }

        [HttpPatch]
        public async Task<OrderDraftDTO> UpdateOrderStatus(UpdateOrderStatusCommand command)
        {
            _logger.LogInformation(
                "Sending command: {CommandName} - ({@Command})",
                "Update Order",
                command);

            return await _mediator.Send(command);
        }
    }
}
