using Application.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SupplierRepository(GazpromTestTaskDbContext context) : ISupplierRepository
{
    private readonly GazpromTestTaskDbContext _context = context;

    public async Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Suppliers.FindAsync(new object?[] { id }, cancellationToken);
    }

    public async Task<IReadOnlyList<Supplier>> GetTopSuppliersAsync(int count, CancellationToken cancellationToken)
    {
        return await _context.Suppliers
            .Include(s => s.Offers)
            .AsNoTracking()
            .OrderByDescending(s => s.Offers.Count)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}
