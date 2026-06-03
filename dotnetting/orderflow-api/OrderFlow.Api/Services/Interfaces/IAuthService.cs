using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;

namespace OrderFlow.Api.Services.Interfaces;

public interface IAuthService
{
    LoginResponse Login(LoginRequest request);
}