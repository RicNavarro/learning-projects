using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Services.Interfaces;

namespace OrderFlow.Api.Controllers{

    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase{

        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetClients(){
            return Ok(_service.GetAll());
        }


        [HttpPost]
        public IActionResult CreateClient(CreateClientRequest client){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = _service.Create(client);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_service.GetWithOrders(id));

    }
}