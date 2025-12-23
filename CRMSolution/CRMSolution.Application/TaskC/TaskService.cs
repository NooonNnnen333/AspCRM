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
            throw new ValidationException(validationResult.Errors);
        }

        var taskId = Guid.NewGuid();

        var taskC = new TaskC(
            taskId,
            request.EmploeesId,
            request.ProductId,
            request.Headline,
            request.DeadLine)
        {
            ClientId = request.ClientId,
            Note = request.Note ?? string.Empty,
            Status = request.Status,
            DateOfCreatedThis = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        await _taskRepository.AddAsync(taskC, cancellationToken);
        _logger.LogInformation("Task create with {taskId}", taskId);

        return taskId;
    }

    public async Task<IReadOnlyCollection<TaskC>> Get(GetTasksDto filter, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(filter.Headline))
        {
            tasks = tasks.Where(t => t.Title.Contains(filter.Headline, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (filter.status.HasValue)
        {
            tasks = tasks.Where(t => t.Status == filter.status.Value).ToList();
        }

        if (filter.DeadLine.HasValue)
        {
            tasks = tasks.Where(t => t.DateOfPlaneDo.Date == filter.DeadLine.Value.Date).ToList();
        }

        return tasks;
    }

    public Task<TaskC?> GetById(Guid taskId, CancellationToken cancellationToken)
    {
        return _taskRepository.GetByIdAsync(taskId, cancellationToken);
    }

    public async Task Update(Guid taskId, UpdateTasksDto request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(taskId, cancellationToken)
                   ?? throw new InvalidOperationException($"Task with id {taskId} not found");

        task.Title = request.Headline;
        task.Note = request.Note;
        task.DateOfPlaneDo = request.DeadLine;

        await _taskRepository.SaveAsync(task, cancellationToken);
    }

    public async Task Delete(Guid taskId, CancellationToken cancellationToken)
    {
        await _taskRepository.DeleteAsync(taskId, cancellationToken);
    }
}
