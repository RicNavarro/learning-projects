using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace OrderFlow.Api.Controllers{

    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientController : ControllerBase{

        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await _service.GetAllAsync());
        }


        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientRequest client)
        {

            var created = await _service.CreateAsync(client);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _service.GetByIdWithOrdersAsync(id);
            return Ok(client);
        }
    }
}