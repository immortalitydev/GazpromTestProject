using Application.Offers.Dtos;
using Application.Offers.Validation;
using Application.Repositories;
using Domain.Entities;

namespace Application.Offers;

public sealed class OfferService(
    IOfferRepository offerRepository, 
    ISupplierRepository supplierRepository,
    CreateOfferRequestValidator createOfferRequestValidator, 
    OfferSearchRequestValidator offerSearchRequestValidator)
    : IOfferService
{
    private const int MaxPageSize = 100;

    public async Task<OfferDto> CreateAsync(CreateOfferRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await createOfferRequestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );
            throw new Exceptions.ValidationException(errors);
        }

        var supplier = await supplierRepository.GetByIdAsync(request.SupplierId, cancellationToken);
        if (supplier is null)
        {
            throw new KeyNotFoundException($"Supplier with id {request.SupplierId} was not found.");
        }

        var offer = new Offer
        {
            Brand = request.Brand.Trim(),
            Model = request.Model.Trim(),
            SupplierId = request.SupplierId,
            RegistrationDate = DateTime.UtcNow
        };

        var created = await offerRepository.AddAsync(offer, cancellationToken);
        var supplierName = created.Supplier?.Name ?? supplier.Name;

        return new OfferDto(
            created.Id,
            created.Brand,
            created.Model,
            created.SupplierId,
            supplierName,
            created.RegistrationDate);
    }

    public async Task<OfferSearchResponse> SearchAsync(OfferSearchRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await offerSearchRequestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );
            throw new Exceptions.ValidationException(errors);
        }

        var (offers, totalCount) = await offerRepository.SearchAsync(
            request.Brand?.Trim(),
            request.Model?.Trim(),
            request.SupplierName?.Trim(),
            request.PageSize,
            request.PageNumber,
            cancellationToken);

        var items = offers
            .Select(o => new OfferDto(
                o.Id,
                o.Brand,
                o.Model,
                o.SupplierId,
                o.Supplier?.Name ?? string.Empty,
                o.RegistrationDate))
            .ToList();

        return new OfferSearchResponse(items, totalCount, request.PageNumber,request. PageSize);
    }
}
