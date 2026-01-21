using CRMSolution.Presenters;

namespace CRMSolution.Application;

public interface IProductsService
{
    Task<Guid> Create(CreateProductsDto request, CancellationToken cancellationToken);
}
