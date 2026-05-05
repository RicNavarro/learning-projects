using OrderFlow.Api.Data;
using OrderFlow.Api.Models;

namespace OrderFlow.Api.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public bool ClientExists(int clientId)
        {
            return _context.Clients.Any(c => c.Id == clientId);
        }

        public Order Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }
    }
}
