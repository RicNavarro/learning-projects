using OrderFlow.Api.Models;
using OrderFlow.Api.DTOs.Responses;

namespace OrderFlow.Api.Mappings
{
    public static class OrderMappings
    {
        public static OrderResponse ToResponse(this Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                Description = order.Description,
                ClientId = order.ClientId,
                Status = order.Status
            };
        }
    }
}