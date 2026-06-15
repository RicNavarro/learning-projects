using OrderFlow.Api.Models;

namespace OrderFlow.Api.Repositories.Interfaces;

public interface IOrderRepository
{
    Order Add(Order order);

    Order? GetById(int id);

    bool Delete(int id);

    bool ClientExists(int clientId);

    void SaveChanges();
}