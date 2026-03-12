namespace Application.Offers.Dtos;

public class OfferSearchRequest
{
    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? SupplierName { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}