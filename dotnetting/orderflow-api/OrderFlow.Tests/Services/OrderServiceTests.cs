using FluentAssertions;
using OrderFlow.Api.Data;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;
using OrderFlow.Tests.Helpers;

public class OrderServiceTests
{
    [Fact]
    public void GetById_ShouldReturnOrder()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var client = new Client
        {
            Name = "Ricardo",
            Email = "ricardo@email.com"
        };

        context.Clients.Add(client);
        context.SaveChanges();

        var order = new Order
        {
            Description = "Pedido Teste",
            ClientId = client.Id
        };

        context.Orders.Add(order);
        context.SaveChanges();

        var service = new OrderService(context);

        // Act

        var result = service.GetById(order.Id);

        // Assert

        result.Should().NotBeNull();

        result!.Description.Should().Be("Pedido Teste");
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var service = new OrderService(context);

        // Act

        var result = service.GetById(999);

        // Assert

        result.Should().BeNull();
    }

    [Fact]
    public void Create_ShouldCreateOrder()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var client = new Client
        {
            Name = "Ricardo",
            Email = "ricardo@email.com"
        };

        context.Clients.Add(client);
        context.SaveChanges();

        var service = new OrderService(context);

        var request = new CreateOrderRequest
        {
            Description = "Pedido Novo",
            ClientId = client.Id
        };

        // Act

        var result = service.Create(request);

        // Assert

        result.Should().NotBeNull();

        result.Description.Should().Be("Pedido Novo");
    }

    [Fact]
    public void Create_ShouldThrowException_WhenClientDoesNotExist()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var service = new OrderService(context);

        var request = new CreateOrderRequest
        {
            Description = "Pedido Inválido",
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
        var context = DbContextFactory.Create();

        var service = new OrderService(context);

        var result = service.Delete(999);

        result.Should().BeFalse();
    }


    [Fact]
    public void Delete_ShouldRemoveOrder()
    {
        var context = DbContextFactory.Create();

        var client = new Client
        {
            Name = "Ricardo",
            Email = "ricardo@email.com"
        };

        context.Clients.Add(client);
        context.SaveChanges();

        var order = new Order
        {
            Description = "Pedido",
            ClientId = client.Id
        };

        context.Orders.Add(order);
        context.SaveChanges();

        var service = new OrderService(context);

        var deleted = service.Delete(order.Id);

        deleted.Should().BeTrue();

        context.Orders.Count().Should().Be(0);
    }
}
