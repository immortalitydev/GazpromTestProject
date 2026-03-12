using Application.Offers.Dtos;
using FluentValidation;

namespace Application.Offers.Validation;

public sealed class CreateOfferRequestValidator : AbstractValidator<CreateOfferRequest>
{
    public CreateOfferRequestValidator()
    {
        RuleFor(x => x.Brand)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Model)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.SupplierId)
            .GreaterThan(0);
    }
}
