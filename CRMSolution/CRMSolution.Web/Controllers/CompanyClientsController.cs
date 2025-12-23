using CRMSolution.Domain.CompanyesClients;
using CRMSolution.Infrastructure.Postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMSolution.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyClientsController : ControllerBase
{
    private readonly TaskDbContext _dbContext;

    public CompanyClientsController(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<CompanyesClients>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _dbContext.CompanyClients.AsNoTracking().ToListAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CompanyesClients>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _dbContext.CompanyClients.AsNoTracking().FirstOrDefaultAsync(x => x.CompanyesClientsId == id, cancellationToken);
        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateCompanyClientRequest request, CancellationToken cancellationToken)
    {
        var item = new CompanyesClients
        {
            CompanyesClientsId = Guid.NewGuid(),
            Mail = request.Mail ?? string.Empty,
            NumberOfPhone = request.NumberOfPhone ?? string.Empty,
            Inn = request.Inn ?? string.Empty
        };

        _dbContext.CompanyClients.Add(item);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = item.CompanyesClientsId }, item.CompanyesClientsId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCompanyClientRequest request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.CompanyClients.FirstOrDefaultAsync(x => x.CompanyesClientsId == id, cancellationToken);
        if (item is null)
        {
            return NotFound();
        }

        item.Mail = request.Mail ?? string.Empty;
        item.NumberOfPhone = request.NumberOfPhone ?? string.Empty;
        item.Inn = request.Inn ?? string.Empty;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var item = await _dbContext.CompanyClients.FirstOrDefaultAsync(x => x.CompanyesClientsId == id, cancellationToken);
        if (item is null)
        {
            return NotFound();
        }

        _dbContext.CompanyClients.Remove(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}

public record CreateCompanyClientRequest(string Mail, string NumberOfPhone, string Inn);

public record UpdateCompanyClientRequest(string Mail, string NumberOfPhone, string Inn);
