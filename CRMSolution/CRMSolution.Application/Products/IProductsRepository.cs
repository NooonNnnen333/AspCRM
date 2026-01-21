using CRMSolution.Domain.Products;

namespace CRMSolution.Application;

public interface IProductsRepository
{
    Task<Guid> AddAsync(Products products, CancellationToken cancellationToken);

    Task<Guid> SaveAsync(Products products, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid productId, CancellationToken cancellationToken);

    Task<Products> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
}
