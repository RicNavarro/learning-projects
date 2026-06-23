using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;

namespace OrderFlow.Api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateAsync(CreateOrderRequest request);

        Task<OrderResponse> GetByIdAsync(int id);

        Task<OrderResponse> UpdateAsync(int id, CreateOrderRequest request);

        Task DeleteAsync(int id);
    }
}