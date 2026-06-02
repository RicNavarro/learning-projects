using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;
using OrderFlow.Api.Services.Interfaces;
using OrderFlow.Api.Mappings;

namespace OrderFlow.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;

        // O ASP.NET vai injetar o DbContext aqui automaticamente
        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public List<ClientResponse> GetAll()
        {
            // O EF Core busca no banco e transforma em lista
            return _context.Clients
                .Include(c => c.Orders)
                .Select(c => c.ToResponse())
                .ToList();
        }

        public ClientResponse Create(CreateClientRequest request)
        {

            var client = new Client
            {
                Name = request.Name,
                Email = request.Email
            };

            _context.Clients.Add(client); // Prepara o INSERT
            _context.SaveChanges();      // Executa no SQL Server
            return client.ToResponse();
        }

        public ClientResponse GetWithOrders(int id)
        {
            var client = _context.Clients
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.Id == id);

            if (client == null)
                return null;

            return client.ToResponse();
        }

    }
}