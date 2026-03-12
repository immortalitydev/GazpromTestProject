namespace Application.Offers.Dtos;

public record OfferSearchResponse(
    IReadOnlyList<OfferDto> Items,
    int TotalCount,
    int PageNumber,
    int PageSize);