using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Services.Interfaces;

namespace OrderFlow.Api.Controllers
{
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
        public IActionResult Create(CreateOrderRequest request) // Alterado para receber o DTO
        {

            var created = _service.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        //READ
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _service.GetById(id);

            if (order == null)
                return NotFound();
            
            return Ok(order);
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(int id, CreateOrderRequest request)
        {
            var updated = _service.Update(id, request);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _service.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

    }
}