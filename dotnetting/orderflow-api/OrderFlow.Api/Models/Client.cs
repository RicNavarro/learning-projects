using System.ComponentModel.DataAnnotations;
// Permite que eu use coisas como [Email address] para validações

namespace OrderFlow.Api.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public List<Order> Orders { get; set; }
    }
}