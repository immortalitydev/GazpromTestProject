using Application.Offers.Dtos;
using FluentValidation;

namespace Application.Offers.Validation;

public sealed class OfferSearchRequestValidator : AbstractValidator<OfferSearchRequest>
{
    public OfferSearchRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100);

        RuleFor(x => x.Brand)
            .MaximumLength(100);

        RuleFor(x => x.Model)
            .MaximumLength(100);

        RuleFor(x => x.SupplierName)
            .MaximumLength(100);
    }
}
