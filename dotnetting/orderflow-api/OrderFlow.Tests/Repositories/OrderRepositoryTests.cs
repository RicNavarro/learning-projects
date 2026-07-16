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
    [Fact]
    public async Task GetPaged_ShouldFilterByStatus()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var client = new Client
        {
            Name = "Cliente Teste"
        };

        context.Clients.Add(client);
        await context.SaveChangesAsync();

        context.Orders.AddRange(
            new Order
            {
                Description = "Pedido Pendente",
                ClientId = client.Id,
                Amount = 100,
                Status = OrderStatus.Pending
            },
            new Order
            {
                Description = "Pedido Concluído",
                ClientId = client.Id,
                Amount = 200,
                Status = OrderStatus.Completed
            });

        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        var request = new GetOrdersRequest
        {
            Status = OrderStatus.Pending,
            Page = 1,
            PageSize = 10
        };

        // Act

        var (orders, totalItems) = await repository.GetPagedAsync(request);

        // Assert

        totalItems.Should().Be(1);

        orders.Should().ContainSingle();

        orders.First().Status.Should().Be(OrderStatus.Pending);
    }

    [Fact]
    public async Task GetPaged_ShouldFilterByDescription()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var client = new Client
        {
            Name = "Cliente Teste"
        };

        context.Clients.Add(client);
        await context.SaveChangesAsync();

        context.Orders.AddRange(
            new Order
            {
                Description = "Notebook Dell",
                ClientId = client.Id,
                Amount = 3000,
                Status = OrderStatus.Pending
            },
            new Order
            {
                Description = "Mouse Gamer",
                ClientId = client.Id,
                Amount = 150,
                Status = OrderStatus.Pending
            });

        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        var request = new GetOrdersRequest
        {
            Description = "Notebook",
            Page = 1,
            PageSize = 10
        };

        // Act

        var (orders, totalItems) = await repository.GetPagedAsync(request);

        // Assert

        totalItems.Should().Be(1);

        orders.Should().ContainSingle();

        orders.First().Description.Should().Contain("Notebook");
    }

    [Fact]
    public async Task GetPaged_ShouldFilterByMinAmount()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var client = new Client
        {
            Name = "Cliente Teste"
        };

        context.Clients.Add(client);
        await context.SaveChangesAsync();

        context.Orders.AddRange(
            new Order
            {
                Description = "Pedido Barato",
                ClientId = client.Id,
                Amount = 100,
                Status = OrderStatus.Pending
            },
            new Order
            {
                Description = "Pedido Caro",
                ClientId = client.Id,
                Amount = 1000,
                Status = OrderStatus.Pending
            });

        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        var request = new GetOrdersRequest
        {
            MinAmount = 500,
            Page = 1,
            PageSize = 10
        };

        // Act

        var (orders, totalItems) = await repository.GetPagedAsync(request);

        // Assert

        totalItems.Should().Be(1);

        orders.Should().ContainSingle();

        orders.First().Amount.Should().Be(1000);
    }

    [Fact]
    public async Task GetPaged_ShouldFilterByMaxAmount()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var client = new Client
        {
            Name = "Cliente Teste"
        };

        context.Clients.Add(client);
        await context.SaveChangesAsync();

        context.Orders.AddRange(
            new Order
            {
                Description = "Pedido Barato",
                ClientId = client.Id,
                Amount = 100,
                Status = OrderStatus.Pending
            },
            new Order
            {
                Description = "Pedido Caro",
                ClientId = client.Id,
                Amount = 1000,
                Status = OrderStatus.Pending
            });

        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        var request = new GetOrdersRequest
        {
            MaxAmount = 200,
            Page = 1,
            PageSize = 10
        };

        // Act

        var (orders, totalItems) = await repository.GetPagedAsync(request);

        // Assert

        totalItems.Should().Be(1);

        orders.Should().ContainSingle();

        orders.First().Amount.Should().Be(100);
    }

    [Fact]
    public async Task GetPaged_ShouldSortByAmountAscending()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var client = new Client
        {
            Name = "Cliente Teste"
        };

        context.Clients.Add(client);
        await context.SaveChangesAsync();

        context.Orders.AddRange(
            new Order
            {
                Description = "Pedido A",
                ClientId = client.Id,
                Amount = 900,
                Status = OrderStatus.Pending
            },
            new Order
            {
                Description = "Pedido B",
                ClientId = client.Id,
                Amount = 200,
                Status = OrderStatus.Pending
            },
            new Order
            {
                Description = "Pedido C",
                ClientId = client.Id,
                Amount = 500,
                Status = OrderStatus.Pending
            });

        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        var request = new GetOrdersRequest
        {
            SortBy = "amount",
            SortDirection = "asc",
            Page = 1,
            PageSize = 10
        };

        // Act

        var (orders, _) = await repository.GetPagedAsync(request);

        // Assert

        orders.Select(o => o.Amount)
            .Should()
            .Equal(200, 500, 900);
    }

    [Fact]
    public async Task GetPaged_ShouldSortByAmountDescending()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var client = new Client
        {
            Name = "Cliente Teste"
        };

        context.Clients.Add(client);
        await context.SaveChangesAsync();

        context.Orders.AddRange(
            new Order
            {
                Description = "Pedido A",
                ClientId = client.Id,
                Amount = 900,
                Status = OrderStatus.Pending
            },
            new Order
            {
                Description = "Pedido B",
                ClientId = client.Id,
                Amount = 200,
                Status = OrderStatus.Pending
            },
            new Order
            {
                Description = "Pedido C",
                ClientId = client.Id,
                Amount = 500,
                Status = OrderStatus.Pending
            });

        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        var request = new GetOrdersRequest
        {
            SortBy = "amount",
            SortDirection = "desc",
            Page = 1,
            PageSize = 10
        };

        // Act

        var (orders, _) = await repository.GetPagedAsync(request);

        // Assert

        orders.Select(o => o.Amount)
            .Should()
            .Equal(900, 500, 200);
    }

}