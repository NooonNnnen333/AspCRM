using CRMSolution.Domain.Task;
using CRMSolution.Infrastructure.Postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMSolution.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskDbContext _dbContext;

    public TasksController(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskC>>> GetAll(CancellationToken cancellationToken)
    {
        var tasks = await _dbContext.Tasks.AsNoTracking().ToListAsync(cancellationToken);
        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskC>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.TaskId == id, cancellationToken);
        if (task is null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var clientExists = await _dbContext.Clients.AnyAsync(x => x.ClientId == request.ClientId, cancellationToken);
        if (!clientExists)
        {
            return BadRequest("Client not found");
        }

        var productExists = await _dbContext.Products.AnyAsync(x => x.ProductId == request.ProductId, cancellationToken);
        if (!productExists)
        {
            return BadRequest("Product not found");
        }

        if (request.EmloyeesId.Count > 0)
        {
            var foundEmployees = await _dbContext.Employees.CountAsync(x => request.EmloyeesId.Contains(x.EmployeesId), cancellationToken);
            if (foundEmployees != request.EmloyeesId.Count)
            {
                return BadRequest("One or more employees not found");
            }
        }

        var taskId = Guid.NewGuid();
        var task = new TaskC(taskId, request.EmloyeesId, request.ProductId, request.Title, request.DateOfPlaneDo)
        {
            ClientId = request.ClientId,
            Note = request.Note ?? string.Empty,
            Status = request.Status,
            DateOfCreatedThis = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        _dbContext.Tasks.Add(task);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = task.TaskId }, task.TaskId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.TaskId == id, cancellationToken);
        if (task is null)
        {
            return NotFound();
        }

        task.Title = request.Title;
        task.Note = request.Note ?? string.Empty;
        task.DateOfPlaneDo = request.DateOfPlaneDo;
        task.Status = request.Status;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.TaskId == id, cancellationToken);
        if (task is null)
        {
            return NotFound();
        }

        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}

public record CreateTaskRequest(
    string Title,
    string? Note,
    DateTime DateOfPlaneDo,
    Guid ClientId,
    Guid ProductId,
    List<Guid> EmloyeesId,
    Status Status = Status.TO_DO);

public record UpdateTaskRequest(
    string Title,
    string? Note,
    DateTime DateOfPlaneDo,
    Status Status);
