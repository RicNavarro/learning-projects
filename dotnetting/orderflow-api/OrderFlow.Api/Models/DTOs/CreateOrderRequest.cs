namespace OrderFlow.Api.DTO;

public class CreateOrderRequest
{
    public string Description { get; set; }
    public int ClientId { get; set; }
}