using FluentValidation;
using OrderFlow.Api.DTOs.Requests;

namespace OrderFlow.Api.DTOs.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição do pedido é obrigatória.")
                .MinimumLength(5).WithMessage("A descrição deve ser mais detalhada.");

            RuleFor(x => x.ClientId)
                .GreaterThan(0).WithMessage("Um cliente válido deve ser associado ao pedido.");

                RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("O valor do pedido deve ser maior que zero.");
        }
    }
}