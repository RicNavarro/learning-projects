using Moq;
using FluentAssertions;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;
using OrderFlow.Tests.Helpers;
using OrderFlow.Api.Repositories.Interfaces;

namespace OrderFlow.Tests.Services;

public class ClientServiceTests
{
    [Fact]
    public void Create_ShouldCreateClient()
    {
        // Arrange

        var repositoryMock = new Mock<IClientRepository>();

        var service = new ClientService(repositoryMock.Object);

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

        repositoryMock.Verify(
            x => x.Add(It.IsAny<Client>()),
            Times.Once);

        repositoryMock.Verify(
            x => x.SaveChanges(),
            Times.Once);
    }

    [Fact]
    public void GetAll_ShouldReturnCreatedClients()
    {
        // Arrange

        var repositoryMock = new Mock<IClientRepository>();

        repositoryMock
            .Setup(x => x.GetAll())
            .Returns(
                new List<Client>
                {
                    new()
                    {
                        Name = "Ricardo",
                        Email = "ricardo@email.com"
                    },
                    new()
                    {
                        Name = "Jayane",
                        Email = "jayane@email.com"
                    }
                }
            );
        
        var service = new ClientService(repositoryMock.Object);

        // Act

        var clients = service.GetAll();

        // Assert

        clients.Should().HaveCount(2);

        clients.Select(c => c.Name)
            .Should()
            .Contain("Ricardo")
            .And.Contain("Jayane");

        repositoryMock.Verify(
            x => x.GetAll(),
            Times.Once);
    }
    
    
    [Fact]
    public void GetByIdWithOrders_ShouldReturnCreatedClient()
    {
        // Arrange

        var repositoryMock = new Mock<IClientRepository>();

        repositoryMock
        .Setup(x => x.GetByIdWithOrders(1))
        .Returns(
            new Client
            {
                Id = 1,
                Name = "Ricardo",
                Email = "ricardo@email.com"
            }
        );

        var service = new ClientService(repositoryMock.Object);

        // Act

        var result = service.GetByIdWithOrders(1);

        // Assert

        result.Should().NotBeNull();

        result!.Name.Should().Be("Ricardo");

        result.Email.Should().Be("ricardo@email.com");

        repositoryMock.Verify(
            x => x.GetByIdWithOrders(1),
            Times.Once);
    }
}