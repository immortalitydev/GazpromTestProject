using Application.Offers.Dtos;

namespace Application.Offers;

public interface IOfferService
{
    Task<OfferDto> CreateAsync(
        CreateOfferRequest request, 
        CancellationToken cancellationToken);

    Task<OfferSearchResponse> SearchAsync(
        OfferSearchRequest request, 
        CancellationToken cancellationToken);
}
