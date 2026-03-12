namespace Application.Suppliers;

public record SupplierDto(
    int Id,
    string Name,
    DateTime CreatedAt,
    int OffersCount);
