using Application.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OfferRepository(GazpromTestTaskDbContext context) : IOfferRepository
{
    public async Task<Offer> AddAsync(Offer offer, CancellationToken cancellationToken)
    {
        await context.Offers.AddAsync(offer, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
        
        await context.Entry(offer)
            .Reference(x => x.Supplier)
            .LoadAsync(cancellationToken);
        
        return offer;
    }

    public async Task<(List<Offer> offers, int totalCount)> SearchAsync(
        string? brand,
        string? model,
        string? supplierName,
        int pageSize,
        int pageNumber,
        CancellationToken cancellationToken)
    {
        var query = context.Offers
            .Include(o => o.Supplier)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(brand))
        {
            query = query.Where(o => EF.Functions.ILike(o.Brand, $"%{brand}%"));
        }

        if (!string.IsNullOrWhiteSpace(model))
        {
            query = query.Where(o => EF.Functions.ILike(o.Model, $"%{model}%"));
        }

        if (!string.IsNullOrWhiteSpace(supplierName))
        {
            query = query.Where(o => EF.Functions.ILike(o.Supplier.Name, $"%{supplierName}%"));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var offers = await query
            .OrderByDescending(o => o.RegistrationDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return (offers, totalCount);
    }
}
