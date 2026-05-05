using System.Text.Json.Serialization;

namespace OrderFlow.Api.Models

{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
    
        [JsonIgnore] // Isso evita que o pedido tente renderizar o cliente de volta
        public Client? Client { get; set; }
    }
}