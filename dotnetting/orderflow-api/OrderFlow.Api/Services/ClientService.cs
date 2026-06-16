using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;
using OrderFlow.Api.Services.Interfaces;
using OrderFlow.Api.Repositories.Interfaces;
using OrderFlow.Api.Mappings;

namespace OrderFlow.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        // O ASP.NET vai injetar o DbContext aqui automaticamente
        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public List<ClientResponse> GetAll()
        {
            // O EF Core busca no banco e transforma em lista
            return _repository.GetAll()
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

            _repository.Add(client); // Prepara o INSERT
            _repository.SaveChanges();      // Executa no SQL Server
            return client.ToResponse();
        }

        public ClientResponse? GetByIdWithOrders(int id)
        {
            var client = _repository.GetByIdWithOrders(id);

            if (client == null)
                return null;

            return client.ToResponse();
        }

    }
}