using CRMSolution.Domain.Client;
using CRMSolution.Infrastructure.Postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMSolution.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly TaskDbContext _dbContext;

    public ClientsController(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Client>>> GetAll(CancellationToken cancellationToken)
    {
        var clients = await _dbContext.Clients.AsNoTracking().ToListAsync(cancellationToken);
        return Ok(clients);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Client>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var client = await _dbContext.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.ClientId == id, cancellationToken);
        if (client is null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateClientRequest request, CancellationToken cancellationToken)
    {
        var client = new Client
        {
            ClientId = Guid.NewGuid(),
            Name = request.Name,
            Famaly = request.Famaly,
            Otchestvo = request.Otchestvo,
            Mail = request.Mail ?? string.Empty,
            NumberOfPhone = request.NumberOfPhone ?? string.Empty,
            NumbersOfReqest = request.NumbersOfReqest,
            Passport = request.Passport
        };

        _dbContext.Clients.Add(client);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = client.ClientId }, client.ClientId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateClientRequest request, CancellationToken cancellationToken)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.ClientId == id, cancellationToken);
        if (client is null)
        {
            return NotFound();
        }

        client.Name = request.Name;
        client.Famaly = request.Famaly;
        client.Otchestvo = request.Otchestvo;
        client.Mail = request.Mail ?? string.Empty;
        client.NumberOfPhone = request.NumberOfPhone ?? string.Empty;
        client.NumbersOfReqest = request.NumbersOfReqest;
        client.Passport = request.Passport;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.ClientId == id, cancellationToken);
        if (client is null)
        {
            return NotFound();
        }

        _dbContext.Clients.Remove(client);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}

public record CreateClientRequest(
    string Name,
    string Famaly,
    string? Otchestvo,
    string Mail,
    string NumberOfPhone,
    int NumbersOfReqest,
    string Passport);

public record UpdateClientRequest(
    string Name,
    string Famaly,
    string? Otchestvo,
    string Mail,
    string NumberOfPhone,
    int NumbersOfReqest,
    string Passport);
