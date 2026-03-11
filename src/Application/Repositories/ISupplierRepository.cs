using Domain.Entities;

namespace Application.Repositories;

public interface ISupplierRepository
{
    Task<Supplier?> GetByIdAsync(
        int id, 
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Supplier>> GetTopSuppliersAsync(
        int count, 
        CancellationToken cancellationToken);
}
