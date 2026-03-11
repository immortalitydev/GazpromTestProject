namespace Application.Suppliers;

public interface ISupplierService
{
    Task<IReadOnlyList<SupplierDto>> GetTopSuppliersAsync(
        int count, 
        CancellationToken cancellationToken);
}
