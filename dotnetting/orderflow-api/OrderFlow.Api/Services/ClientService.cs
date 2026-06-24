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
using OrderFlow.Api.Exceptions;


namespace OrderFlow.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly ILogger<ClientService> _logger;

        // O ASP.NET vai injetar o DbContext aqui automaticamente
        public ClientService(
            IClientRepository repository,
            ILogger<ClientService> logger)
        {
            _repository = repository;
            _logger = logger;
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

            _logger.LogInformation("Criando cliente com email {Email}", request.Email);


            await _repository.AddAsync(client); // perpara o insert
            await _repository.SaveChangesAsync(); // adiciona no banco

            return client.ToResponse();
        }

        public async Task<ClientResponse> GetByIdWithOrdersAsync(int id)
        {
            var client = await _repository.GetByIdWithOrdersAsync(id);

            if (client == null)
            {
                _logger.LogWarning("Cliente {ClientId} não encontrado.", id);
                throw new NotFoundException($"Cliente {id} não encontrado.");

            }
            
            return client.ToResponse();
        }

    }
}