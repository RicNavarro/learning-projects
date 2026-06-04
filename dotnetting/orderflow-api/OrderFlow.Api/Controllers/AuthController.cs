using Microsoft.AspNetCore.Mvc;

using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;

namespace OrderFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        return Ok(_service.Login(request));
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            Email = User.Identity?.Name,

            Claims = User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            })
        });
    }


}

