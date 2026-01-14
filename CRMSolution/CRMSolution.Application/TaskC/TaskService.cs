using CRMSolution.Application.Exceptions;
using CRMSolution.Application.Extension;
using CRMSolution.Domain.Task;
using CRMSolution.Presenters;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace CRMSolution.Application;

public class TaskService : ITaskService
{

    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<TaskC> _logger;
    private readonly IValidator<CreateTaskDto> _validator;

    public TaskService(ITaskRepository iTask, IValidator<CreateTaskDto> validator, ILogger<TaskC> logger)
    {
        _taskRepository = iTask;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Guid> Create(CreateTaskDto request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            //throw new TaskValidationException(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
            throw new TaskValidationException(validationResult.ToErrors());
        }

        var taskId = Guid.NewGuid();
        var taskC = new TaskC(
            taskId,
            request.EmploeesId,
            request.ProductId,
            request.Headline,
            request.DeadLine);



        await _taskRepository.AddAsync(taskC, cancellationToken);
        _logger.LogInformation("Task create with {taskId}", taskId);

        return request.TaskId;
    }

    // public async Task<IActionResult> Get([FromQuery] GetTasksDto tasksDto, CancellationToken cancellationToken)
    // {
    //     return Ok();
    // }
    //
    // public async Task<IActionResult> GetByTask([FromRoute] Guid usersId, CancellationToken cancellationToken)
    // {
    //     return Ok();
    // }
    //
    // public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] UpdateTasksDto request, CancellationToken cancellationToken)
    // {
    //     return Ok();
    // }
    //
    // public async Task<IActionResult> Delite([FromRoute] Guid taskId, [FromRoute] Guid userId, CancellationToken cancellationToken)
    // {
    //     return Ok();
    // }
}