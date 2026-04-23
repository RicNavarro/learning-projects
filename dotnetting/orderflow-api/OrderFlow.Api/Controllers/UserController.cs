using Microsoft.AspNetCore.Mvc;

namespace OrderFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = new List<string>
            {
                "Sonic",
                "Tails",
                "Knuckles"
            };

            return Ok(users);
        }
    }
}