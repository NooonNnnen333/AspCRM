using CRMSolution.Domain.Task;
using CRMSolution.Presenters;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CRMSolution.Application;

public class TaskService : ITaskService
{

    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<TaskC> _logger;
    private readonly IValidator<CreateTaskDto> _validator;
    private readonly IValidator<UpdateTasksDto> _updateValidator;

    public TaskService(
        ITaskRepository iTask,
        IValidator<CreateTaskDto> validator,
        IValidator<UpdateTasksDto> updateValidator,
        ILogger<TaskC> logger)
    {
        _taskRepository = iTask;
        _validator = validator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    public async Task<Guid> Create(CreateTaskDto request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
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

        return taskId;
    }

    public async Task<IReadOnlyList<TaskC>> GetAll(CancellationToken cancellationToken)
    {
        return await _taskRepository.GetAllAsync(cancellationToken);
    }

    public async Task<TaskC> GetById(Guid taskId, CancellationToken cancellationToken)
    {
        return await _taskRepository.GetByIdAsync(taskId, cancellationToken);
    }

    public async Task<Guid> Update(Guid taskId, UpdateTasksDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _updateValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var task = await _taskRepository.GetByIdAsync(taskId, cancellationToken);
        task.Title = request.Headline;
        task.Note = request.Note;
        task.DateOfPlaneDo = request.DeadLine;

        await _taskRepository.SaveAsync(task, cancellationToken);
        _logger.LogInformation("Task updated with {taskId}", taskId);

        return taskId;
    }

    public async Task<Guid> Delete(Guid taskId, CancellationToken cancellationToken)
    {
        await _taskRepository.DeleteAsync(taskId, cancellationToken);
        _logger.LogInformation("Task deleted with {taskId}", taskId);

        return taskId;
    }
}
