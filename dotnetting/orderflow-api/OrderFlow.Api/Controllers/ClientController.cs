using Microsoft.AspNetCore.Mvc;

namespace OrderFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetClients()
        {
            var clients = new List<string>
            {
                "Ricardo",
                "Jayane",
                "Yelena"
            };

            return Ok(clients);
        }
    }
}