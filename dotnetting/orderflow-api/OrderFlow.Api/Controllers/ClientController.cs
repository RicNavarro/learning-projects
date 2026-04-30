using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Models;

namespace OrderFlow.Api.Controllers{

    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase{

        private static List<Client> clients = new List<Client>();

        [HttpGet]
        public IActionResult GetClients(){

            //var clients = new List<string>
            //{
            //    "Ricardo",
            //    "Jayane",
            //    "Yelena"
            //};

            return Ok(clients);
        }

        [HttpPost]
        public IActionResult CreateClient(Client client){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            client.Id = clients.Count + 1;
            clients.Add(client);

            return Created("", client);
        }


    }
}