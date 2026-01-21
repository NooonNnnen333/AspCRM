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
}
