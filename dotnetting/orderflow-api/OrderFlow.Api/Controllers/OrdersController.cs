using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;
using OrderFlow.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace OrderFlow.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        //CREATE
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderRequest request)
        {
            var created = await _service.CreateAsync(request);

            return CreatedAtAction(nameof(GetById),
                new { id = created.Id },
                created);
        }


        // READ

        /// <summary>
        /// Lista pedidos com suporte a paginação, filtros e ordenação.
        /// </summary>
        /// <param name="request">
        /// Parâmetros utilizados para paginação, filtros e ordenação.
        /// </param>
        /// <returns>Uma lista paginada de pedidos.</returns>
        /// <response code="200">Pedidos retornados com sucesso.</response>
        /// <response code="400">Parâmetros inválidos.</response>


        [HttpGet]
        public async Task<ActionResult<PagedResponse<OrderResponse>>> GetOrders(
            [FromQuery] GetOrdersRequest request)
        {
            var response = await _service.GetPagedAsync(request);

            return Ok(response);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateOrderRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);
            return Ok(updated);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderResponse>> GetById(int id)
        {
            var order = await _service.GetByIdAsync(id);

            return Ok(order);
        }


        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

    }
}