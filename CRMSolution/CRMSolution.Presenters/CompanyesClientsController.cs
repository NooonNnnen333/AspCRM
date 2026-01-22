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

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var companyesClients = await _service.GetAll(cancellationToken);
        return Ok(companyesClients);
    }

    [HttpGet("{companyesClientsId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid companyesClientsId, CancellationToken cancellationToken)
    {
        var companyesClients = await _service.GetById(companyesClientsId, cancellationToken);
        return Ok(companyesClients);
    }

    [HttpPut("{companyesClientsId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid companyesClientsId,
        [FromBody] UpdateCompanyesClientsDto request,
        CancellationToken cancellationToken)
    {
        await _service.Update(companyesClientsId, request, cancellationToken);
        return Ok();
    }

    [HttpDelete("{companyesClientsId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid companyesClientsId, CancellationToken cancellationToken)
    {
        await _service.Delete(companyesClientsId, cancellationToken);
        return Ok();
    }
}
