namespace OrderFlow.Api.DTOs.Requests;

public class CreateOrderRequest
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int ClientId { get; set; }
}