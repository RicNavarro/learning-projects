using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;

namespace OrderFlow.Api.Services.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientResponse>> GetAllAsync();

        Task<ClientResponse> CreateAsync(CreateClientRequest request);

        Task<ClientResponse?> GetByIdWithOrdersAsync(int id);
    }
}