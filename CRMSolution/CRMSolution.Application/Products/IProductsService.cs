using CRMSolution.Domain.Products;
using CRMSolution.Presenters;

namespace CRMSolution.Application;

public interface IProductsService
{
    Task<Guid> Create(CreateProductsDto request, CancellationToken cancellationToken);

    Task<IReadOnlyList<Products>> GetAll(CancellationToken cancellationToken);

    Task<Products> GetById(Guid productId, CancellationToken cancellationToken);

    Task<Guid> Update(Guid productId, UpdateProductsDto request, CancellationToken cancellationToken);

    Task<Guid> Delete(Guid productId, CancellationToken cancellationToken);
}
