namespace Application.Offers.Dtos;

public record OfferDto(
    int Id,
    string Brand,
    string Model,
    int SupplierId,
    string SupplierName,
    DateTime RegistrationDate);