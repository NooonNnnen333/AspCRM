using CRMSolution.Domain.Products;
using CRMSolution.Infrastructure.Postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMSolution.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly TaskDbContext _dbContext;

    public ProductsController(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Products>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _dbContext.Products.AsNoTracking().ToListAsync(cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Products>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id, cancellationToken);
        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Products
        {
            ProductId = Guid.NewGuid(),
            NameOfProduct = request.NameOfProduct
        };

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product.ProductId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id, cancellationToken);
        if (product is null)
        {
            return NotFound();
        }

        product.NameOfProduct = request.NameOfProduct;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id, cancellationToken);
        if (product is null)
        {
            return NotFound();
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}

public record CreateProductRequest(string NameOfProduct);

public record UpdateProductRequest(string NameOfProduct);
