using CRMSolution.Application;
using CRMSolution.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CRMSolution.Infrastructure.Postgres.Repositories;

public class ProductsEfCoreRepository : IProductsRepository
{
    private readonly TaskDbContext _dbContext;

    public ProductsEfCoreRepository(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(Products products, CancellationToken cancellationToken)
    {
        await _dbContext.products.AddAsync(products, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return products.ProductId;
    }

    public async Task<Guid> SaveAsync(Products products, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.products.AnyAsync(x => x.ProductId == products.ProductId, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException($"Products {products.ProductId} not found");
        }

        _dbContext.products.Update(products);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return products.ProductId;
    }

    public async Task<Guid> DeleteAsync(Guid productId, CancellationToken cancellationToken)
    {
        var products = await _dbContext.products.FindAsync(new object[] { productId }, cancellationToken);
        if (products is null)
        {
            throw new KeyNotFoundException($"Products {productId} not found");
        }

        _dbContext.products.Remove(products);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return productId;
    }

    public async Task<Products> GetByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var products = await _dbContext.products.AsNoTracking()
            .FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken);

        return products ?? throw new KeyNotFoundException($"Products {productId} not found");
    }

    public async Task<IReadOnlyList<Products>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.products.AsNoTracking().ToListAsync(cancellationToken);
    }
}
