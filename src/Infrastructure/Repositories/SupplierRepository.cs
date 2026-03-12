using Application.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SupplierRepository(GazpromTestTaskDbContext context) : ISupplierRepository
{
    public async Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Suppliers.FindAsync(new object?[] { id }, cancellationToken);
    }

    public async Task<IReadOnlyList<Supplier>> GetTopSuppliersAsync(int count, CancellationToken cancellationToken)
    {
        return await context.Suppliers
            .Include(s => s.Offers)
            .AsNoTracking()
            .OrderByDescending(s => s.Offers.Count)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}
