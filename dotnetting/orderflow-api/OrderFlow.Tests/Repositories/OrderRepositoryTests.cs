using FluentAssertions;
using OrderFlow.Api.Models;
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
}