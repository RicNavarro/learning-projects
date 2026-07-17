using OrderFlow.Api.Models;

namespace OrderFlow.Tests.Builders;

public class OrderBuilder
{
    private readonly Order _order = new()
    {
        Description = "Pedido Teste",
        Amount = 100,
        Status = OrderStatus.Pending
    };

    public OrderBuilder WithDescription(string description)
    {
        _order.Description = description;
        return this;
    }

    public OrderBuilder WithAmount(decimal amount)
    {
        _order.Amount = amount;
        return this;
    }

    public OrderBuilder WithStatus(OrderStatus status)
    {
        _order.Status = status;
        return this;
    }

    public OrderBuilder WithClient(int clientId)
    {
        _order.ClientId = clientId;
        return this;
    }

    public Order Build()
    {
        return _order;
    }
}