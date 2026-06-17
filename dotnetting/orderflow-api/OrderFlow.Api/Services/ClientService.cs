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

        public async Task<List<ClientResponse>> GetAllAsync()
        {
            // O EF Core busca no banco e transforma em lista
            return (await _repository.GetAllAsync())
                .Select(c => c.ToResponse())
                .ToList();
        }

        public async Task<ClientResponse> CreateAsync(CreateClientRequest request)
        {

            var client = new Client
            {
                Name = request.Name,
                Email = request.Email
            };

            await _repository.AddAsync(client); // perpara o insert
            await _repository.SaveChangesAsync(); // adiciona no banco
            return client.ToResponse();
        }

        public async Task<ClientResponse?> GetByIdWithOrdersAsync(int id)
        {
            var client = await _repository.GetByIdWithOrdersAsync(id);

            if (client == null)
                return null;

            return client.ToResponse();
        }

    }
}