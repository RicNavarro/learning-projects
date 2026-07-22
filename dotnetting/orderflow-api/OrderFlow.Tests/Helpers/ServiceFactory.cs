using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OrderFlow.Api.Configuration;
using OrderFlow.Api.Repositories.Interfaces;
using OrderFlow.Api.Services;

namespace OrderFlow.Tests.Helpers;

public static class ServiceFactory
{
    public static OrderService CreateOrderService(
        Mock<IOrderRepository> repositoryMock)
    {
        var logger = new Mock<ILogger<OrderService>>();

        var memoryCache = new MemoryCache(
            new MemoryCacheOptions());

        var cacheOptions = Options.Create(new CacheOptions
        {
            OrdersExpirationMinutes = 5
        });

        return new OrderService(
            repositoryMock.Object,
            logger.Object,
            memoryCache,
            cacheOptions);
    }
}