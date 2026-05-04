using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrderFlow.Api.Services
{
    public class ClientService
    {
        private readonly AppDbContext _context;

        // O ASP.NET vai injetar o DbContext aqui automaticamente
        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public List<Client> GetAll()
        {
            // O EF Core busca no banco e transforma em lista
            return _context.Clients.ToList();
        }

        public Client Create(Client client)
        {
            _context.Clients.Add(client); // Prepara o INSERT
            _context.SaveChanges();      // Executa no SQL Server
            return client;
        }
    }
}