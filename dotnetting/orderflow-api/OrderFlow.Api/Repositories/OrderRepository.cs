using Microsoft.EntityFrameworkCore;
using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using OrderFlow.Api.Repositories.Interfaces;
using OrderFlow.Api.DTOs.Requests;

namespace OrderFlow.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Order> AddAsync(Order order)
    {
        _context.Orders.Add(order);

        return Task.FromResult(order);
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<bool> ClientExistsAsync(int clientId)
    {
        return await _context.Clients
            .AnyAsync(c => c.Id == clientId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order =
            await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            return false;

        _context.Orders.Remove(order);

        return true;
    }

    public async Task<(IEnumerable<Order> Orders, int TotalItems)> GetPagedAsync(
        GetOrdersRequest request)
    {
        var query = _context.Orders
            .Include(o => o.Client)
            .AsQueryable();


        query = ApplyFilters(query, request);

        query = ApplySorting(query, request);

        var totalItems = await query.CountAsync();

        var orders = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return (orders, totalItems);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }


    private IQueryable<Order> ApplyFilters(
    IQueryable<Order> query,
    GetOrdersRequest request)
    {
        if (request.ClientId.HasValue)
        {
            query = query.Where(o =>
                o.ClientId == request.ClientId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            query = query.Where(o =>
                EF.Functions.Like(
                    o.Description,
                    $"%{request.Description}%"));
        }

        return query;
    }

    private IQueryable<Order> ApplySorting(
        IQueryable<Order> query,
        GetOrdersRequest request)
    {
        var descending = request.SortDirection?.Equals(
            "desc",
            StringComparison.OrdinalIgnoreCase) == true;

        return request.SortBy?.ToLower() switch
        {
            "description" => descending
                ? query.OrderByDescending(o => o.Description)
                : query.OrderBy(o => o.Description),

            "clientid" => descending
                ? query.OrderByDescending(o => o.ClientId)
                : query.OrderBy(o => o.ClientId),

            _ => descending
                ? query.OrderByDescending(o => o.Id)
                : query.OrderBy(o => o.Id)
        };
    }

}