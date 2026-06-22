using Moq;
using FluentAssertions;
using OrderFlow.Api.Data;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;
using OrderFlow.Tests.Helpers;
using OrderFlow.Api.Repositories.Interfaces;
using OrderFlow.Api.Exceptions;

public class OrderServiceTests
{
    [Fact]
    public async Task GetById_ShouldReturnOrder()
    {
        // Arrange

        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(
            new Order
            {
                Id = 1,
                Description = "Pedido Teste",
                ClientId = 1
            });

        var service = new OrderService(repositoryMock.Object);

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

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange

        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();
        
        repositoryMock.Setup
            (x => x.GetByIdAsync(999))
            .ReturnsAsync((Order?)null);

        var service = new OrderService(repositoryMock.Object);

        // Act

        var result = await service.GetByIdAsync(999);

        // Assert

        result.Should().BeNull();

        repositoryMock.Verify(
            x => x.GetByIdAsync(999),
            Times.Once);
    }

    [Fact]
    public async Task Create_ShouldCreateOrder()
    {
        // Arrange

        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x => x.ClientExistsAsync(1))
            .ReturnsAsync(true);

        var service = new OrderService(repositoryMock.Object);

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

        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x=>x.ClientExistsAsync(999))
            .ReturnsAsync(false);

        var service = new OrderService(repositoryMock.Object);

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
    public async Task Delete_ShouldReturnFalse_WhenOrderDoesNotExist()
    {
        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x=>x.DeleteAsync(999))
            .ReturnsAsync(false);

        var service = new OrderService(repositoryMock.Object);

        var result = await service.DeleteAsync(999);

        result.Should().BeFalse();
        
        repositoryMock.Verify(
            x => x.DeleteAsync(999),
            Times.Once);
    }


    [Fact]
    public async Task Delete_ShouldRemoveOrder()
    {

        //Arrange
        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x=>x.DeleteAsync(1))
            .ReturnsAsync(true);

        var service = new OrderService(repositoryMock.Object);

        //Act

        var deleted = await service.DeleteAsync(1);

        //Assert

        deleted.Should().BeTrue();

        repositoryMock.Verify(
            x => x.DeleteAsync(1),
            Times.Once);

        repositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }
}
