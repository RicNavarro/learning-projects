using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;

namespace OrderFlow.Api.Services.Interfaces
{
    public interface IOrderService
    {
        OrderResponse Create(CreateOrderRequest request);

        OrderResponse? GetById(int id);

        OrderResponse? Update(int id, CreateOrderRequest request);

        bool Delete(int id);
    }
}