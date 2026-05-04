using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;

namespace OrderFlow.Api.Controllers{

    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase{

        private readonly ClientService _service;

        // O Controller pede o Service para o .NET
        public ClientController(ClientService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetClients(){
            return Ok(_service.GetAll());
        }


        [HttpPost]
        public IActionResult CreateClient(Client client){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = _service.Create(client);
            return Created("", created);
        }


    }
}