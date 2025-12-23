using CRMSolution.Application;
using Microsoft.AspNetCore.Mvc;

namespace CRMSolution.Presenters;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _iService;

    public TaskController(ITaskService iService)
    {
        _iService = iService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto request, CancellationToken cancellationToken)
    {
        var taskId = await _iService.Create(request, cancellationToken);
        return Ok(taskId);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetTasksDto tasksDto, CancellationToken cancellationToken)
    {
        var result = await _iService.Get(tasksDto, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid taskId, CancellationToken cancellationToken)
    {
        var task = await _iService.GetById(taskId, cancellationToken);
        if (task is null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpPut("{taskId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid taskId, [FromBody] UpdateTasksDto request, CancellationToken cancellationToken)
    {
        await _iService.Update(taskId, request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid taskId, CancellationToken cancellationToken)
    {
        await _iService.Delete(taskId, cancellationToken);
        return NoContent();
    }
}
