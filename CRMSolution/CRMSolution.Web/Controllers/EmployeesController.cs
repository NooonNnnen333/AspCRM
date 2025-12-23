using CRMSolution.Domain.Emploees;
using CRMSolution.Infrastructure.Postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMSolution.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly TaskDbContext _dbContext;

    public EmployeesController(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employees>>> GetAll(CancellationToken cancellationToken)
    {
        var employees = await _dbContext.Employees.AsNoTracking().ToListAsync(cancellationToken);
        return Ok(employees);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Employees>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var employee = await _dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.EmployeesId == id, cancellationToken);
        if (employee is null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var employee = new Employees
        {
            EmployeesId = Guid.NewGuid(),
            Name = request.Name,
            Famaly = request.Famaly,
            Otchestvo = request.Otchestvo,
            Mail = request.Mail ?? string.Empty,
            Role = request.Role,
            NumberOfPhone = request.NumberOfPhone ?? string.Empty
        };

        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = employee.EmployeesId }, employee.EmployeesId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeesId == id, cancellationToken);
        if (employee is null)
        {
            return NotFound();
        }

        employee.Name = request.Name;
        employee.Famaly = request.Famaly;
        employee.Otchestvo = request.Otchestvo;
        employee.Mail = request.Mail ?? string.Empty;
        employee.Role = request.Role;
        employee.NumberOfPhone = request.NumberOfPhone ?? string.Empty;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeesId == id, cancellationToken);
        if (employee is null)
        {
            return NotFound();
        }

        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}

public record CreateEmployeeRequest(
    string Name,
    string Famaly,
    string? Otchestvo,
    string Mail,
    Role Role,
    string NumberOfPhone);

public record UpdateEmployeeRequest(
    string Name,
    string Famaly,
    string? Otchestvo,
    string Mail,
    Role Role,
    string NumberOfPhone);
