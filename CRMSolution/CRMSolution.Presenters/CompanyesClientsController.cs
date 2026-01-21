using CRMSolution.Application;
using Microsoft.AspNetCore.Mvc;

namespace CRMSolution.Presenters;

[ApiController]
[Route("[controller]")]
public class CompanyesClientsController : ControllerBase
{
    private readonly ICompanyesClientsService _service;

    public CompanyesClientsController(ICompanyesClientsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyesClientsDto request, CancellationToken cancellationToken)
    {
        await _service.Create(request, cancellationToken);
        return Ok();
    }
}
