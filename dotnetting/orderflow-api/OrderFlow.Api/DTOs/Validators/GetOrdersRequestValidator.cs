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
    }
}