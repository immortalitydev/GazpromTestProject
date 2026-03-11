namespace Application.Offers;

public record OfferDto(
    int Id,
    string Brand,
    string Model,
    int SupplierId,
    string SupplierName,
    DateTime RegistrationDate);

public class CreateOfferRequest
{
    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int SupplierId { get; set; }
}

public class OfferSearchRequest
{
    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? SupplierName { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}

public record OfferSearchResponse(
    IReadOnlyList<OfferDto> Items,
    int TotalCount,
    int PageNumber,
    int PageSize);
