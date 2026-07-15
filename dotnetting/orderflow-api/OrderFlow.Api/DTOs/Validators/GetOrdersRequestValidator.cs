using FluentValidation;
using OrderFlow.Api.DTOs.Requests;

namespace OrderFlow.Api.DTOs.Validators;

public class GetOrdersRequestValidator : AbstractValidator<GetOrdersRequest>
{
    public GetOrdersRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("A página deve ser maior que zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("O tamanho da página deve ser maior que zero.");

        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(100)
            .WithMessage("O tamanho máximo da página é 100.");

        RuleFor(x => x.ClientId)
            .GreaterThan(0)
            .When(x => x.ClientId.HasValue)
            .WithMessage("O cliente deve ser maior que zero.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .When(x => x.Status.HasValue)
            .WithMessage("Status inválido.");

        RuleFor(x => x.SortDirection)
            .Must(direction =>
                direction == null ||
                direction.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
                direction.Equals("desc", StringComparison.OrdinalIgnoreCase))
            .WithMessage("SortDirection deve ser 'asc' ou 'desc'.");

        RuleFor(x => x.MinAmount)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinAmount.HasValue)
                .WithMessage("O valor mínimo deve ser maior ou igual a zero.");
                
        RuleFor(x => x.MaxAmount)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MaxAmount.HasValue)
            .WithMessage("O valor máximo deve ser maior ou igual a zero.");

        RuleFor(x => x)
            .Must(x =>
                !x.MinAmount.HasValue ||
                !x.MaxAmount.HasValue ||
                x.MinAmount <= x.MaxAmount)
            .WithMessage("O valor mínimo não pode ser maior que o valor máximo.");
    }
}