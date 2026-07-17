using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;
using OrderFlow.Api.Services.Interfaces;
using OrderFlow.Api.Mappings;
using OrderFlow.Api.Repositories.Interfaces;
using OrderFlow.Api.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace OrderFlow.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<OrderService> _logger;
        private readonly IMemoryCache _cache;
        private static CancellationTokenSource _ordersCacheTokenSource = new();

        public OrderService(
            IOrderRepository repository,
            ILogger<OrderService> logger,
            IMemoryCache cache)
        {
            _repository = repository;
            _logger = logger;
            _cache = cache;
        }


        // CREATE

        public async Task<OrderResponse> CreateAsync(CreateOrderRequest request)
        {

            //Validando se o cliente existe antes de criar o pedido
            var clientExists = await _repository.ClientExistsAsync(request.ClientId);
            
            if (!clientExists)
            {
                _logger.LogWarning("Cliente {ClientId} não foi encontrado enquanto criando pedido", request.ClientId);                
                throw new NotFoundException($"O cliente {request.ClientId} não existe");
            }
            
            var order = new Order
            {
                Description = request.Description,
                ClientId = request.ClientId,
                Status = OrderStatus.Pending,
                Amount = request.Amount
            };

            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();
            InvalidateOrdersCache();

            _logger.LogInformation("Pedido {OrderId} para cliente {ClientId}", order.Id, request.ClientId);

            return order.ToResponse();
        }





        // READ

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null)
            {
                _logger.LogWarning("Pedido {OrderId} não encontrado.",id);
                throw new NotFoundException($"Pedido {id} não encontrado.");
            }
            
            return order.ToResponse();
        }

        public async Task<PagedResponse<OrderResponse>> GetPagedAsync(GetOrdersRequest request)
        {
            _logger.LogInformation(
                "Buscando pedidos. Página {Page}, Tamanho {PageSize}",
                request.Page,
                request.PageSize);

            var cacheKey =
                $"orders:{request.Page}:{request.PageSize}:{request.ClientId}:{request.Description}:{request.SortBy}:{request.SortDirection}";

            if (_cache.TryGetValue(cacheKey, out PagedResponse<OrderResponse>? cachedResponse))
            {
                _logger.LogInformation(
                    "Cache HIT para {CacheKey}",
                    cacheKey);

                return cachedResponse!;
            }

            _logger.LogInformation(
                "Cache MISS para {CacheKey}",
                cacheKey);


            var (orders, totalItems) = await _repository.GetPagedAsync(request);

            var response = PagedResponse<OrderResponse>.Create(
                orders.Select(o => o.ToResponse()),
                request.Page,
                request.PageSize,
                totalItems);

            _cache.Set(
                cacheKey,
                response,
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    .AddExpirationToken(
                        new CancellationChangeToken(
                            _ordersCacheTokenSource.Token)));

            return response;
        }



        // UPDATE

        public async Task<OrderResponse> UpdateAsync(int id, CreateOrderRequest request)
        {

            var order = await _repository.GetByIdAsync(id);
            if (order == null)
            {
                _logger.LogWarning("Pedido {OrderId} não encontrado durante atualização.", id);
                throw new NotFoundException($"Pedido {id} não encontrado.");
            }


            var clientExists = await _repository.ClientExistsAsync(request.ClientId);
            if (!clientExists)
            {
                _logger.LogWarning("Cliente {ClientId} não encontrado durante atualização do pedido {OrderId}", request.ClientId, id);
                throw new NotFoundException($"O cliente {request.ClientId} não existe");
            }


            order.Description = request.Description;
            order.ClientId = request.ClientId;
            order.Amount = request.Amount;

            await _repository.SaveChangesAsync();
            InvalidateOrdersCache();

            return order.ToResponse();
        }




        // DELETE

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
            {
                _logger.LogWarning("Pedido {OrderId} não encontrado durante remoção.", id);
                throw new NotFoundException($"Pedido {id} não encontrado.");
            }
                
            await _repository.SaveChangesAsync();
            InvalidateOrdersCache();

        }




        private void InvalidateOrdersCache()
        {
            _ordersCacheTokenSource.Cancel();
            _ordersCacheTokenSource.Dispose();

            _ordersCacheTokenSource = new CancellationTokenSource();

            _logger.LogInformation("Cache de pedidos invalidado.");
        }

    }
}
