using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderFlow.Api.Controllers;
using OrderFlow.Api.DTOs.Responses;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.Services.Interfaces;

namespace OrderFlow.Tests.Controllers;

public class OrdersControllerTests
{
    [Fact]
    public void GetById_ShouldReturnOk_WhenOrderExists()
    {
        // Arrange

        var expectedOrder =
            new OrderResponse
            {
                Id = 1,
                Description = "Pedido Teste"
            };

        var serviceMock =
            new Mock<IOrderService>();

        serviceMock
            .Setup(x => x.GetById(1))
            .Returns(expectedOrder);

        var controller =
            new OrdersController(
                serviceMock.Object);

        // Act

        var result =
            controller.GetById(1);

        // Assert

        var okResult =
            result.Should()
                .BeOfType<OkObjectResult>()
                .Subject;

        var order =
            okResult.Value
                    .Should()
                    .BeOfType<OrderResponse>()
                    .Subject;

        order.Id.Should().Be(1);

        order.Description.Should().Be("Pedido Teste");

        serviceMock.Verify(
            x => x.GetById(1),
            Times.Once);

    }

    [Fact]
    public void GetById_ShouldReturnNotFound_WhenOrderDoesNotExist()
    {
        var serviceMock =
            new Mock<IOrderService>();

        serviceMock
            .Setup(x => x.GetById(999))
            .Returns((OrderResponse?)null);

        var controller =
            new OrdersController(
                serviceMock.Object);

        var result =
            controller.GetById(999);

        result.Should()
            .BeOfType<NotFoundResult>();
        
        serviceMock.Verify(
            x => x.GetById(999),
            Times.Once);
    }

    [Fact]
    public void Create_ShouldReturnCreatedAtAction(){

        var serviceMock = new Mock<IOrderService>();

        serviceMock
            .Setup(x => x.Create(It.IsAny<CreateOrderRequest>()))
            .Returns(
                new OrderResponse
                {
                    Id = 10,
                    Description = "Pedido Novo"
                }
            );

        var controller = new OrdersController(serviceMock.Object);

        var request = new CreateOrderRequest{
            Description = "Pedido Novo",
            ClientId = 1
        };

        var result = controller.Create(request);

        // Assertion

        var createdResult =
            result.Should()
                .BeOfType<CreatedAtActionResult>()
                .Subject;

        var order =
            createdResult.Value
                .Should()
                .BeOfType<OrderResponse>()
                .Subject;

        order.Id.Should().Be(10);

        order.Description.Should().Be("Pedido Novo");

        serviceMock.Verify(
            x => x.Create(It.IsAny<CreateOrderRequest>()),
            Times.Once);

    }

    [Fact]
    public void Delete_ShouldReturnNotFound_WhenOrderDoesNotExist(){

        var serviceMock = new Mock<IOrderService>();

        serviceMock
            .Setup(x => x.Delete(1))
            .Returns(false);

        var controller =
            new OrdersController(
                serviceMock.Object
            );
        
        //

        var result = controller.Delete(1);

        //

        result.Should()
            .BeOfType<NotFoundResult>();

        serviceMock.Verify(
            x => x.Delete(1),
            Times.Once);

    }

    [Fact]
    public void Delete_ShouldReturnNoContent_WhenOrderExists(){

        var serviceMock = new Mock<IOrderService>();

        serviceMock
            .Setup(x => x.Delete(1))
            .Returns(true);

        var controller =
            new OrdersController(
                serviceMock.Object
            );
        
        //

        var result = controller.Delete(1);

        //

        result.Should()
            .BeOfType<NoContentResult>();

        serviceMock.Verify(
            x => x.Delete(1),
            Times.Once);


    }
    

}

