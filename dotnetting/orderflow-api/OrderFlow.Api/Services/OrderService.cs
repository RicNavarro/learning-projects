using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;
using OrderFlow.Api.Services.Interfaces;
using OrderFlow.Api.Mappings;

namespace OrderFlow.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }


        // CREATE

        public OrderResponse Create(CreateOrderRequest request)
        {

            //Validando se o cliente existe antes de criar o pedido
            var clientExists = _context.Clients.Any(c => c.Id == request.ClientId);
            if (!clientExists)
                throw new ArgumentException($"O cliente {request.ClientId} não existe");

            
            var order = new Order
            {
                Description = request.Description,
                ClientId = request.ClientId
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.ToResponse();
        }





        // READ

        public OrderResponse? GetById(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
                return null;
        //        throw new KeyNotFoundException($"Pedido {id} não existe");

            return order.ToResponse();
        }





        // UPDATE

        public OrderResponse? Update(int id, CreateOrderRequest request)
        {

            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return null;
        //        throw new KeyNotFoundException($"Pedido {id} não existe");


            var clientExists = _context.Clients.Any(c => c.Id == request.ClientId);
            if (!clientExists)
                throw new ArgumentException($"O cliente {request.ClientId} não existe");


            order.Description = request.Description;
            order.ClientId = request.ClientId;

            _context.SaveChanges();

            return order.ToResponse();
        }




        // DELETE

        public bool Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
                return false;
            //    throw new KeyNotFoundException($"Pedido {id} não existe");

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return true;
        }



    }
}
