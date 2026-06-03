using Microsoft.AspNetCore.Mvc;

using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Services.Interfaces;

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

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        return Ok(_service.Login(request));
    }
}