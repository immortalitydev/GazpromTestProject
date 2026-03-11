using Domain.Entities;

namespace Application.Repositories;

public interface IOfferRepository
{
    Task<Offer> AddAsync(
        Offer offer, 
        CancellationToken cancellationToken);

    Task<(List<Offer> offers, int totalCount)> SearchAsync(
        string? brand,
        string? model,
        string? supplierName,
        int pageSize,
        int pageNumber,
        CancellationToken cancellationToken);
}
