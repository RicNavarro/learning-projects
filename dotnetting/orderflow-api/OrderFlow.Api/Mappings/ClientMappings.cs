using OrderFlow.Api.Models;
using OrderFlow.Api.DTOs.Responses;

namespace OrderFlow.Api.Mappings
{
    public static class ClientMappings
    {
        public static ClientResponse ToResponse(this Client client)
        {
            return new ClientResponse
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Orders = client.Orders.Select(o => o.ToResponse())
                .ToList()
            };
        }
    }
}