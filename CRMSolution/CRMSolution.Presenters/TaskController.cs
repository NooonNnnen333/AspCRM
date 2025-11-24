using CRMSolution.Application;
using CRMSolution.Domain.Client;
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
        return Ok();
    }

    [HttpGet("{usersId:guid}")]
    public async Task<IActionResult> GetByTask([FromRoute] Guid usersId, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] UpdateTasksDto request, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpDelete("{taskId}/{userId}")]
    public async Task<IActionResult> Delite([FromRoute] Guid taskId, [FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        return Ok();
    }
}