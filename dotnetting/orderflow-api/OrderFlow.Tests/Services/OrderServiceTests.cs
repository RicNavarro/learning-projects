using Moq;
using FluentAssertions;
using OrderFlow.Api.Data;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;
using OrderFlow.Tests.Helpers;
using OrderFlow.Api.Repositories.Interfaces;

public class OrderServiceTests
{
    [Fact]
    public void GetById_ShouldReturnOrder()
    {
        // Arrange

        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock.Setup(x => x.GetById(1)).Returns(
            new Order
            {
                Id = 1,
                Description = "Pedido Teste",
                ClientId = 1
            });

        var service = new OrderService(repositoryMock.Object);

        // Act

        var result = service.GetById(1);

        // Assert

        result.Should().NotBeNull();

        result!.Description.Should().Be("Pedido Teste");

        repositoryMock.Verify(
            x => x.GetById(1),
            Times.Once
        );
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange

        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();
        
        repositoryMock.Setup
            (x => x.GetById(999))
            .Returns((Order?)null);

        var service = new OrderService(repositoryMock.Object);

        // Act

        var result = service.GetById(999);

        // Assert

        result.Should().BeNull();

        repositoryMock.Verify(
            x => x.GetById(999),
            Times.Once);
    }

    [Fact]
    public void Create_ShouldCreateOrder()
    {
        // Arrange

        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x => x.ClientExists(1))
            .Returns(true);

        var service = new OrderService(repositoryMock.Object);

        var request = new CreateOrderRequest
        {
            Description = "Pedido Novo",
            ClientId = 1
        };

        // Act

        var result = service.Create(request);

        // Assert

        result.Should().NotBeNull();

        result.Description.Should().Be("Pedido Novo");

        repositoryMock.Verify(
            x => x.Add(It.IsAny<Order>()),
            Times.Once);
        
        repositoryMock.Verify(
           x => x.SaveChanges(),
            Times.Once);
    }

    [Fact]
    public void Create_ShouldThrowException_WhenClientDoesNotExist()
    {
        // Arrange

        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x=>x.ClientExists(999))
            .Returns(false);

        var service = new OrderService(repositoryMock.Object);

        var request =
        new CreateOrderRequest
        {
            Description = "Pedido Novo",
            ClientId = 999
        };

        // Act

        Action action = () => service.Create(request);

        // Assert

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*não existe*");
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenOrderDoesNotExist()
    {
        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x=>x.Delete(999))
            .Returns(false);

        var service = new OrderService(repositoryMock.Object);

        var result = service.Delete(999);

        result.Should().BeFalse();
        
        repositoryMock.Verify(
            x => x.Delete(999),
            Times.Once);
    }


    [Fact]
    public void Delete_ShouldRemoveOrder()
    {

        //Arrange
        //var context = DbContextFactory.Create();
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x=>x.Delete(1))
            .Returns(true);

        var service = new OrderService(repositoryMock.Object);

        //Act

        var deleted = service.Delete(1);

        //Assert

        deleted.Should().BeTrue();

        repositoryMock.Verify(
            x => x.Delete(1),
            Times.Once);

        repositoryMock.Verify(
            x => x.SaveChanges(),
            Times.Once);
    }
}
