using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Dishes.Commands;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public DishController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<OrderDraftDTO> Add(AddDishCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                "Add Dish",
                nameof(command.BuyerId),
                command.BuyerId,
                command);

            return await _mediator.Send(command);
        }

        [HttpPatch]
        public async Task<OrderDraftDTO> Update(UpdateDishCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Sending command: {CommandName}: ({@Command})",
                "Update Dish",
                command);

            return await _mediator.Send(command);
        }

        [HttpDelete]
        public async Task Delete([FromQuery] Guid Id, CancellationToken cancellationToken)
        {
            var command = new DeleteDishCommand(Id);

            _logger.LogInformation(
                "Sending command: {CommandName}: ({@Command})",
                "Delete Dish",
                command);

            await _mediator.Send(command);
        }
    }
}
