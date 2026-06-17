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

    public async Task<List<Client>> GetAllAsync()
    {
        return await _context.Clients
            .Include(c => c.Orders)
            .ToListAsync();
    }

    public Task<Client> AddAsync(Client client)
    {
        _context.Clients.Add(client);

        return Task.FromResult(client);
    }

    public async Task<Client?> GetByIdWithOrdersAsync(int id)
    {
        return await _context.Clients
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}