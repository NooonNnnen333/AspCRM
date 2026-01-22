using CRMSolution.Application;
using Microsoft.AspNetCore.Mvc;

namespace CRMSolution.Presenters;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _service;

    public ProductsController(IProductsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductsDto request, CancellationToken cancellationToken)
    {
        await _service.Create(request, cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var products = await _service.GetAll(cancellationToken);
        return Ok(products);
    }

    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid productId, CancellationToken cancellationToken)
    {
        var products = await _service.GetById(productId, cancellationToken);
        return Ok(products);
    }

    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid productId,
        [FromBody] UpdateProductsDto request,
        CancellationToken cancellationToken)
    {
        await _service.Update(productId, request, cancellationToken);
        return Ok();
    }

    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid productId, CancellationToken cancellationToken)
    {
        await _service.Delete(productId, cancellationToken);
        return Ok();
    }
}
