using FluentValidation;
using OrderFlow.Api.DTOs.Requests;

namespace OrderFlow.Api.DTOs.Validators
{
    public class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
    {
        public CreateClientRequestValidator(){
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(100).WithMessage("O nome não pode ultrapassar 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }

    }
}