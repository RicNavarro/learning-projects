using OrderFlow.Api.Models;

namespace OrderFlow.Api.DTOs.Requests;

public class GetOrdersRequest
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int? ClientId { get; set; }

    public string? Description { get; set; }

    public OrderStatus? Status { get; set; }

    public string? SortBy { get; set; }

    public string? SortDirection { get; set; }
}