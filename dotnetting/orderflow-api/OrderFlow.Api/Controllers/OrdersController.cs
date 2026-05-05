using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;
using OrderFlow.Api.DTO;

namespace OrderFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service;

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create(CreateOrderRequest request) // Alterado para receber o DTO
        {
            // A validação continua usando o ID que vem do DTO
            if (!_service.ClientExists(request.ClientId))
                return BadRequest("Client does not exist");

            // Mapeamos o DTO para a Entidade antes de enviar ao Service
            var order = new Order
            {
                Description = request.Description,
                ClientId = request.ClientId
            };

            var created = _service.Create(order);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}