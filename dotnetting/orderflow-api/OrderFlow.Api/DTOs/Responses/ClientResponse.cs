using System.Collections.Generic;

namespace OrderFlow.Api.DTOs.Responses
{
    public class ClientResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<OrderResponse> Orders { get; set; } = new();
    }
}