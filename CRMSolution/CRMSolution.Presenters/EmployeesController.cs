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

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var employees = await _service.GetAll(cancellationToken);
        return Ok(employees);
    }

    [HttpGet("{employeesId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid employeesId, CancellationToken cancellationToken)
    {
        var employees = await _service.GetById(employeesId, cancellationToken);
        return Ok(employees);
    }

    [HttpPut("{employeesId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid employeesId,
        [FromBody] UpdateEmployeesDto request,
        CancellationToken cancellationToken)
    {
        await _service.Update(employeesId, request, cancellationToken);
        return Ok();
    }

    [HttpDelete("{employeesId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid employeesId, CancellationToken cancellationToken)
    {
        await _service.Delete(employeesId, cancellationToken);
        return Ok();
    }
}
