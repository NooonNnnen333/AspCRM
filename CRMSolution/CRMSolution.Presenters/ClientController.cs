using CRMSolution.Application;
using Microsoft.AspNetCore.Mvc;

namespace CRMSolution.Presenters;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _service;

    public ClientController(IClientService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientDto request, CancellationToken cancellationToken)
    {
        await _service.Create(request, cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var clients = await _service.GetAll(cancellationToken);
        return Ok(clients);
    }

    [HttpGet("{clientId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid clientId, CancellationToken cancellationToken)
    {
        var client = await _service.GetById(clientId, cancellationToken);
        return Ok(client);
    }

    [HttpPut("{clientId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid clientId,
        [FromBody] UpdateClientDto request,
        CancellationToken cancellationToken)
    {
        await _service.Update(clientId, request, cancellationToken);
        return Ok();
    }

    [HttpDelete("{clientId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid clientId, CancellationToken cancellationToken)
    {
        await _service.Delete(clientId, cancellationToken);
        return Ok();
    }
}
