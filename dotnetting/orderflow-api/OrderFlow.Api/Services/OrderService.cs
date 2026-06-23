using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;
using OrderFlow.Api.Services.Interfaces;
using OrderFlow.Api.Mappings;
using OrderFlow.Api.Repositories.Interfaces;
using OrderFlow.Api.Exceptions;

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

        public async Task<OrderResponse> CreateAsync(CreateOrderRequest request)
        {

            //Validando se o cliente existe antes de criar o pedido
            var clientExists = await _repository.ClientExistsAsync(request.ClientId);
            if (!clientExists)
                throw new NotFoundException($"O cliente {request.ClientId} não existe");

            
            var order = new Order
            {
                Description = request.Description,
                ClientId = request.ClientId
            };

            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();

            return order.ToResponse();
        }





        // READ

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null)
                throw new NotFoundException($"Pedido {id} não encontrado.");
        //        return null;


            return order.ToResponse();
        }





        // UPDATE

        public async Task<OrderResponse> UpdateAsync(int id, CreateOrderRequest request)
        {

            var order = await _repository.GetByIdAsync(id);
            if (order == null)
                throw new NotFoundException($"Pedido {id} não encontrado.");
                //return null;


            var clientExists = await _repository.ClientExistsAsync(request.ClientId);
            if (!clientExists)
                throw new NotFoundException($"O cliente {request.ClientId} não existe");


            order.Description = request.Description;
            order.ClientId = request.ClientId;

            await _repository.SaveChangesAsync();

            return order.ToResponse();
        }




        // DELETE

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                throw new NotFoundException($"Pedido {id} não encontrado.");

            await _repository.SaveChangesAsync();

        }



    }
}
