using FluentAssertions;
using OrderFlow.Api.Models;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Repositories;
using OrderFlow.Tests.Helpers;

namespace OrderFlow.Tests.Repositories;

public class OrderRepositoryTests
{
    [Fact]
    public async Task GetById_ShouldReturnOrder_WhenOrderExists()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var client = new Client
        {
            Name = "Cliente Teste"
        };

        context.Clients.Add(client);
        await context.SaveChangesAsync();


        var order = new Order
        {
            Description = "Notebook Dell",
            ClientId = client.Id,
            Amount = 5000,
            Status = OrderStatus.Pending
        };

        context.Orders.Add(order);
        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        // Act

        var result = await repository.GetByIdAsync(order.Id);

        // Assert

        result.Should().NotBeNull();

        result!.Description.Should().Be("Notebook Dell");
    }

    [Fact]
    public async Task GetPaged_ShouldFilterByClientId()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var client1 = new Client
        {
            Name = "Cliente 1"
        };

        var client2 = new Client
        {
            Name = "Cliente 2"
        };

        context.Clients.AddRange(client1, client2);
        await context.SaveChangesAsync();

        context.Orders.AddRange(
            new Order
            {
                Description = "Notebook",
                ClientId = client1.Id,
                Amount = 2000,
                Status = OrderStatus.Pending
            },
            new Order
            {
                Description = "Mouse",
                ClientId = client2.Id,
                Amount = 100,
                Status = OrderStatus.Pending
            });

        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        var request = new GetOrdersRequest
        {
            ClientId = client1.Id,
            Page = 1,
            PageSize = 10
        };

        // Act

        var (orders, totalItems) = await repository.GetPagedAsync(request);

        // Assert

        totalItems.Should().Be(1);

        orders.Should().HaveCount(1);

        orders.First().Description.Should().Be("Notebook");
    }
}