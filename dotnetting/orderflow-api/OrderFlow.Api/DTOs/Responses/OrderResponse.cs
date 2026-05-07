namespace OrderFlow.Api.DTOs.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int ClientId { get; set; }
    }
}