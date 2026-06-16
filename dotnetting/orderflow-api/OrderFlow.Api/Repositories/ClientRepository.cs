using Microsoft.EntityFrameworkCore;
using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using OrderFlow.Api.Repositories.Interfaces;

namespace OrderFlow.Api.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Client> GetAll()
    {
        return _context.Clients
            .Include(c => c.Orders)
            .ToList();
    }

    public Client Add(Client client)
    {
        _context.Clients.Add(client);

        return client;
    }

    public Client? GetByIdWithOrders(int id)
    {
        return _context.Clients
            .Include(c => c.Orders)
            .FirstOrDefault(c => c.Id == id);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}