using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using OrderFlow.Api.Repositories.Interfaces;

namespace OrderFlow.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public Order Add(Order order)
    {
        _context.Orders.Add(order);

        return order;
    }

    public Order? GetById(int id)
    {
        return _context.Orders
            .FirstOrDefault(o => o.Id == id);
    }

    public bool ClientExists(int clientId)
    {
        return _context.Clients
            .Any(c => c.Id == clientId);
    }

    public bool Delete(int id)
    {
        var order =
            _context.Orders
                .FirstOrDefault(o => o.Id == id);

        if (order == null)
            return false;

        _context.Orders.Remove(order);

        return true;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

}