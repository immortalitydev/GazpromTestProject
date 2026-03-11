using Application.Repositories;

namespace Application.Suppliers;

public sealed class SupplierService(ISupplierRepository supplierRepository) : ISupplierService
{
    private const int MaxTopCount = 20;
    private const int DefaultTopCount = 3;

    public async Task<IReadOnlyList<SupplierDto>> GetTopSuppliersAsync(
        int count, 
        CancellationToken cancellationToken)
    {
        var normalizedCount = count < 1 ? DefaultTopCount : count;
        if (normalizedCount > MaxTopCount)
        {
            normalizedCount = MaxTopCount;
        }

        var suppliers = await supplierRepository.GetTopSuppliersAsync(
            normalizedCount, 
            cancellationToken);

        return suppliers
            .Select(s => new SupplierDto(
                s.Id,
                s.Name,
                s.CreatedAt,
                s.Offers.Count))
            .ToList();
    }
}
