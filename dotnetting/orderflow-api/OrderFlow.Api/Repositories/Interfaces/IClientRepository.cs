using OrderFlow.Api.Models;

namespace OrderFlow.Api.Repositories.Interfaces;

public interface IClientRepository
{
    List<Client> GetAll();

    Client Add(Client client);

    Client? GetByIdWithOrders(int id);

    void SaveChanges();
}