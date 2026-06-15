using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;
using OrderFlow.Api.Services.Interfaces;
using OrderFlow.Api.Mappings;
using OrderFlow.Api.Repositories.Interfaces;

namespace OrderFlow.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }


        // CREATE

        public OrderResponse Create(CreateOrderRequest request)
        {

            //Validando se o cliente existe antes de criar o pedido
            var clientExists = _repository.ClientExists(request.ClientId);
            if (!clientExists)
                throw new ArgumentException($"O cliente {request.ClientId} não existe");

            
            var order = new Order
            {
                Description = request.Description,
                ClientId = request.ClientId
            };

            _repository.Add(order);
            _repository.SaveChanges();

            return order.ToResponse();
        }





        // READ

        public OrderResponse? GetById(int id)
        {
            var order = _repository.GetById(id);

            if (order == null)
                return null;
        //        throw new KeyNotFoundException($"Pedido {id} não existe");

            return order.ToResponse();
        }





        // UPDATE

        public OrderResponse? Update(int id, CreateOrderRequest request)
        {

            var order = _repository.GetById(id);
            if (order == null)
                return null;
        //        throw new KeyNotFoundException($"Pedido {id} não existe");


            var clientExists = _repository.ClientExists(request.ClientId);
            if (!clientExists)
                throw new ArgumentException($"O cliente {request.ClientId} não existe");


            order.Description = request.Description;
            order.ClientId = request.ClientId;

            _repository.SaveChanges();

            return order.ToResponse();
        }




        // DELETE

        public bool Delete(int id)
        {
            var deleted = _repository.Delete(id);

            if (!deleted)
                return false;
            //    throw new KeyNotFoundException($"Pedido {id} não existe");

            _repository.SaveChanges();

            return true;
        }



    }
}
