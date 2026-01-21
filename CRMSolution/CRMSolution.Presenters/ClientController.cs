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
}
