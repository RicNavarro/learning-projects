using Moq;
using FluentAssertions;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Models;
using OrderFlow.Api.Services;
using OrderFlow.Tests.Helpers;
using OrderFlow.Api.Repositories.Interfaces;
using OrderFlow.Api.Exceptions;

namespace OrderFlow.Tests.Services;

public class ClientServiceTests
{
    [Fact]
    public async Task Create_ShouldCreateClient()
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

        var result = await service.CreateAsync(request);

        // Assert

        result.Should().NotBeNull();

        result.Name.Should().Be("Ricardo");

        result.Email.Should().Be("ricardo@email.com");

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Client>()),
            Times.Once);

        repositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task GetAll_ShouldReturnCreatedClients()
    {
        // Arrange

        var repositoryMock = new Mock<IClientRepository>();

        repositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(
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

        var clients = await service.GetAllAsync();

        // Assert

        clients.Should().HaveCount(2);

        clients.Select(c => c.Name)
            .Should()
            .Contain("Ricardo")
            .And.Contain("Jayane");

        repositoryMock.Verify(
            x => x.GetAllAsync(),
            Times.Once);
    }
    
    
    [Fact]
    public async Task GetByIdWithOrders_ShouldReturnCreatedClient()
    {
        // Arrange

        var repositoryMock = new Mock<IClientRepository>();

        repositoryMock
        .Setup(x => x.GetByIdWithOrdersAsync(1))
        .ReturnsAsync(
            new Client
            {
                Id = 1,
                Name = "Ricardo",
                Email = "ricardo@email.com"
            }
        );

        var service = new ClientService(repositoryMock.Object);

        // Act

        var result = await service.GetByIdWithOrdersAsync(1);

        // Assert

        result.Should().NotBeNull();

        result!.Name.Should().Be("Ricardo");

        result.Email.Should().Be("ricardo@email.com");

        repositoryMock.Verify(
            x => x.GetByIdWithOrdersAsync(1),
            Times.Once);
    }
}