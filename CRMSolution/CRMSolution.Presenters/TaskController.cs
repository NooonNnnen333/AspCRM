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
        await _iService.Create(request, cancellationToken);
        return Ok();
    }

    // [HttpGet]
    // public async Task<IActionResult<List<TaskC>>> GetTasks()
    // {
    //     return Ok()
    // }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetTasksDto tasksDto, CancellationToken cancellationToken)
    {
        var tasks = await _iService.GetAll(cancellationToken);
        return Ok(tasks);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetByTask([FromRoute] Guid taskId, CancellationToken cancellationToken)
    {
        var task = await _iService.GetById(taskId, cancellationToken);
        return Ok(task);
    }

    [HttpPut("{taskId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid taskId, [FromBody] UpdateTasksDto request, CancellationToken cancellationToken)
    {
        await _iService.Update(taskId, request, cancellationToken);
        return Ok();
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> Delite([FromRoute] Guid taskId, CancellationToken cancellationToken)
    {
        await _iService.Delete(taskId, cancellationToken);
        return Ok();
    }
}
