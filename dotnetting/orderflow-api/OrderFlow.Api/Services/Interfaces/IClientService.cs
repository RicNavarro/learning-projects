using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;

namespace OrderFlow.Api.Services.Interfaces
{
    public interface IClientService
    {
        List<ClientResponse> GetAll();

        ClientResponse Create(CreateClientRequest request);

        ClientResponse GetWithOrders(int id);

    }
}