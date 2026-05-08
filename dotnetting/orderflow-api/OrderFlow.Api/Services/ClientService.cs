using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;
using OrderFlow.Api.Services.Interfaces;

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
                .Select(c => MapToResponse(c))
                .ToList();
        }

        public ClientResponse Create(CreateClientRequest request)
        {

            var client = new Client
            {
                Name = request.Name
            };

            _context.Clients.Add(client); // Prepara o INSERT
            _context.SaveChanges();      // Executa no SQL Server
            return MapToResponse(client);
        }

        public ClientResponse GetWithOrders(int id)
        {
            var client = _context.Clients
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.Id == id);

            if (client == null)
                throw new KeyNotFoundException($"Cliente {id} não encontrado.");

            return MapToResponse(client);
        }

        private static ClientResponse MapToResponse(Client client)
        {
            return new ClientResponse
            {
                Id = client.Id,
                Name = client.Name,
                Orders = client.Orders.Select(o => new OrderResponse
                {
                    Id = o.Id,
                    Description = o.Description,
                    ClientId = o.ClientId
                }).ToList()
            };
        }
    }
}