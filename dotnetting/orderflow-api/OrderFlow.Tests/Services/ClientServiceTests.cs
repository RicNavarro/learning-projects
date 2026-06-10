using FluentAssertions;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;
using OrderFlow.Tests.Helpers;

namespace OrderFlow.Tests.Services;

public class ClientServiceTests
{
    [Fact]
    public void Create_ShouldCreateClient()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var service = new ClientService(context);

        var request = new CreateClientRequest
        {
            Name = "Ricardo",
            Email = "ricardo@email.com"
        };

        // Act

        var result = service.Create(request);

        // Assert

        result.Should().NotBeNull();

        result.Name.Should().Be("Ricardo");

        result.Email.Should().Be("ricardo@email.com");
    }

    [Fact]
    public void GetAll_ShouldReturnCreatedClients()
    {
        // Arrange

        var context = DbContextFactory.Create();

        var service = new ClientService(context);

        service.Create(new CreateClientRequest
        {
            Name = "Ricardo",
            Email = "ricardo@email.com"
        });

        service.Create(new CreateClientRequest
        {
            Name = "Jayane",
            Email = "jayane@email.com"
        });

        // Act

        var clients = service.GetAll();

        // Assert

        clients.Should().HaveCount(2);

        clients.Select(c => c.Name)
            .Should()
            .Contain("Ricardo")
            .And.Contain("Jayane");
    }
    
    
    [Fact]
    public void GetWithOrders_ShouldReturnCreatedClient()
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

        var service = new ClientService(context);

        // Act

        var result = service.GetWithOrders(client.Id);

        // Assert

        result.Should().NotBeNull();

        result!.Name.Should().Be("Ricardo");

        result.Email.Should().Be("ricardo@email.com");
    }
}