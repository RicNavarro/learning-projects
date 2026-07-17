using Moq;
using FluentAssertions;
using OrderFlow.Api.Data;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;
using OrderFlow.Tests.Helpers;
using OrderFlow.Api.Repositories.Interfaces;
using OrderFlow.Api.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

public class OrderServiceTests
{
    [Fact]
    public async Task GetById_ShouldReturnOrder()
    {
        // Arrange

        var loggerMock = new Mock<ILogger<OrderService>>();

        

        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(
            new Order
            {
                Id = 1,
                Description = "Pedido Teste",
                ClientId = 1
            });

        var service = new OrderService(
            repositoryMock.Object,
            loggerMock.Object,
            CreateMemoryCache());

        // Act

        var result = await service.GetByIdAsync(1);

        // Assert

        result.Should().NotBeNull();

        result!.Description.Should().Be("Pedido Teste");

        repositoryMock.Verify(
            x => x.GetByIdAsync(1),
            Times.Once
        );
    }

    private static IMemoryCache CreateMemoryCache()
    {
        return new MemoryCache(new MemoryCacheOptions());
    }

    [Fact]
    public async Task GetById_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
    {
        var repositoryMock = new Mock<IOrderRepository>();

        var loggerMock = new Mock<ILogger<OrderService>>();

        

        repositoryMock
            .Setup(x => x.GetByIdAsync(999))
            .ReturnsAsync((Order?)null);

        var service = new OrderService(
            repositoryMock.Object,
            loggerMock.Object,
            CreateMemoryCache());

        Func<Task> action = async () =>
            await service.GetByIdAsync(999);

        await action.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage("*não encontrado*");
    }

    [Fact]
    public async Task Create_ShouldCreateOrder()
    {
        // Arrange

        var loggerMock = new Mock<ILogger<OrderService>>();

        

        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x => x.ClientExistsAsync(1))
            .ReturnsAsync(true);

        var service = new OrderService(
            repositoryMock.Object,
            loggerMock.Object,
            CreateMemoryCache());

        var request = new CreateOrderRequest
        {
            Description = "Pedido Novo",
            ClientId = 1
        };

        // Act

        var result = await service.CreateAsync(request);

        // Assert

        result.Should().NotBeNull();

        result.Description.Should().Be("Pedido Novo");

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Order>()),
            Times.Once);
        
        repositoryMock.Verify(
           x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task Create_ShouldThrowException_WhenClientDoesNotExist()
    {
        // Arrange

        var loggerMock = new Mock<ILogger<OrderService>>();

        

        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x=>x.ClientExistsAsync(999))
            .ReturnsAsync(false);

        var service = new OrderService(
            repositoryMock.Object,
            loggerMock.Object,
            CreateMemoryCache());

        var request =
        new CreateOrderRequest
        {
            Description = "Pedido Novo",
            ClientId = 999
        };

        // Act

        Func<Task> action = async () =>
            await service.CreateAsync(request);

        // Assert

        await action.Should()
            .ThrowAsync<NotFoundException>().
            WithMessage("*não existe*");
    }

    [Fact]
    public async Task Delete_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
    {
        var repositoryMock = new Mock<IOrderRepository>();

        var loggerMock = new Mock<ILogger<OrderService>>();

        

        repositoryMock
            .Setup(x => x.DeleteAsync(999))
            .ReturnsAsync(false);

        var service = new OrderService(
            repositoryMock.Object,
            loggerMock.Object,
            CreateMemoryCache());

        Func<Task> action = async () =>
            await service.DeleteAsync(999);

        await action.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage("*não encontrado*");
    }


    [Fact]
    public async Task Delete_ShouldRemoveOrder()
    {

        //Arrange
        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();

        var loggerMock = new Mock<ILogger<OrderService>>();

        

        repositoryMock
            .Setup(x=>x.DeleteAsync(1))
            .ReturnsAsync(true);

        var service = new OrderService(
            repositoryMock.Object,
            loggerMock.Object,
            CreateMemoryCache());
            
        //Act

        Func<Task> action = async () =>
            await service.DeleteAsync(1);

        await action.Should().NotThrowAsync();

        repositoryMock.Verify(
            x => x.DeleteAsync(1),
            Times.Once);

        repositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task GetPaged_ShouldReturnPagedResponse()
    {
        // Arrange

        var repositoryMock = new Mock<IOrderRepository>();

        var loggerMock = new Mock<ILogger<OrderService>>();

        var orders = new List<Order>
        {
            new()
            {
                Id = 1,
                Description = "Pedido 1",
                ClientId = 1,
                Client = new Client
                {
                    Id = 1,
                    Name = "Cliente Teste"
                }
            },
            new()
            {
                Id = 2,
                Description = "Pedido 2",
                ClientId = 1,
                Client = new Client
                {
                    Id = 1,
                    Name = "Cliente Teste"
                }
            }
        };

        repositoryMock
            .Setup(x => x.GetPagedAsync(
                It.Is<GetOrdersRequest>(r =>
                    r.Page == 1 &&
                    r.PageSize == 10)))
            .ReturnsAsync((orders, 12));

        var service = new OrderService(
            repositoryMock.Object,
            loggerMock.Object,
            CreateMemoryCache());

        var request = new GetOrdersRequest
        {
            Page = 1,
            PageSize = 10
        };

        // Act

        var result = await service.GetPagedAsync(request);

        // Assert

        result.Should().NotBeNull();

        result.Items.Should().HaveCount(2);

        result.Page.Should().Be(1);

        result.PageSize.Should().Be(10);

        result.TotalItems.Should().Be(12);

        result.TotalPages.Should().Be(2);

        repositoryMock.Verify(
            x => x.GetPagedAsync(
                It.Is<GetOrdersRequest>(r =>
                    r.Page == 1 &&
                    r.PageSize == 10)),
            Times.Once);
    }

    
    [Fact]
    public async Task GetPaged_ShouldPassPaginationParametersToRepository()
    {
        // Arrange

        var repositoryMock = new Mock<IOrderRepository>();
        var loggerMock = new Mock<ILogger<OrderService>>();

        repositoryMock
            .Setup(x => x.GetPagedAsync(
                It.Is<GetOrdersRequest>(r =>
                    r.Page == 3 &&
                    r.PageSize == 25)))
            .ReturnsAsync((new List<Order>(), 50));

        var service = new OrderService(
            repositoryMock.Object,
            loggerMock.Object,
            CreateMemoryCache());

        var request = new GetOrdersRequest
        {
            Page = 3,
            PageSize = 25
        };

        // Act

        await service.GetPagedAsync(request);

        // Assert

        repositoryMock.Verify(
            x => x.GetPagedAsync(
                It.Is<GetOrdersRequest>(r =>
                    r.Page == 3 &&
                    r.PageSize == 25)),
            Times.Once);
    }

    [Fact]
    public async Task Create_ShouldAcceptPositiveAmount()
    {
        // Arrange

        var repositoryMock = new Mock<IOrderRepository>();
        var loggerMock = new Mock<ILogger<OrderService>>();

        repositoryMock
            .Setup(x => x.ClientExistsAsync(1))
            .ReturnsAsync(true);

        var service = new OrderService(
            repositoryMock.Object,
            loggerMock.Object,
            CreateMemoryCache());

        var request = new CreateOrderRequest
        {
            Description = "Pedido Teste",
            ClientId = 1,
            Amount = 150.50m
        };

        // Act

        var result = await service.CreateAsync(request);

        // Assert

        result.Amount.Should().Be(150.50m);
    }

    [Theory]
    [InlineData(20, 10, 2)]
    [InlineData(21, 10, 3)]
    [InlineData(0, 10, 0)]
    [InlineData(1, 10, 1)]
    [InlineData(99, 25, 4)]
    public async Task GetPaged_ShouldCalculateTotalPages(
        int totalItems,
        int pageSize,
        int expectedPages)
    {
        // Arrange

        var repositoryMock = new Mock<IOrderRepository>();
        var loggerMock = new Mock<ILogger<OrderService>>();

        repositoryMock
            .Setup(x => x.GetPagedAsync(It.IsAny<GetOrdersRequest>()))
            .ReturnsAsync((new List<Order>(), totalItems));

        var service = new OrderService(
            repositoryMock.Object,
            loggerMock.Object,
            CreateMemoryCache());

        var request = new GetOrdersRequest
        {
            Page = 1,
            PageSize = pageSize
        };

        // Act

        var result = await service.GetPagedAsync(request);

        // Assert

        result.TotalPages.Should().Be(expectedPages);
    }


}
