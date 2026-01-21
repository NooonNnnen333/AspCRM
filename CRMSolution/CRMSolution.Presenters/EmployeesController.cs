using CRMSolution.Application;
using Microsoft.AspNetCore.Mvc;

namespace CRMSolution.Presenters;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeesService _service;

    public EmployeesController(IEmployeesService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeesDto request, CancellationToken cancellationToken)
    {
        await _service.Create(request, cancellationToken);
        return Ok();
    }
}
