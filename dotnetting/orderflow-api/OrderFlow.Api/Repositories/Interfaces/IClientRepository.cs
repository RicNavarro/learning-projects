using OrderFlow.Api.Models;

namespace OrderFlow.Api.Repositories.Interfaces;

public interface IClientRepository
{
    Task<List<Client>> GetAllAsync();

    Task<Client> AddAsync(Client client);

    Task<Client?> GetByIdWithOrdersAsync(int id);

    Task SaveChangesAsync();
}