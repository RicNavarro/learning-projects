namespace OrderFlow.Api.DTOs.Requests;

public class CreateClientRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}