using OrderFlow.Api.Models;

namespace OrderFlow.Api.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Order> AddAsync(Order order);

    Task<Order?> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);

    Task<bool> ClientExistsAsync(int clientId);

    Task<(IEnumerable<Order> Orders, int TotalItems)> GetPagedAsync(
        int page,
        int pageSize);

    Task SaveChangesAsync();
}