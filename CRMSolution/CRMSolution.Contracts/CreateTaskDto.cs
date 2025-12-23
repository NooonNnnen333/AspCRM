using CRMSolution.Domain.Task;

namespace CRMSolution.Presenters;

public record CreateTaskDto(string Headline, string Note, DateTime DeadLine, Guid ClientId, List<Guid> EmploeesId, Guid ProductId, Status Status = Status.TO_DO);

public record GetTasksDto(string? Headline, Status? status, DateTime? DeadLine);

public record UpdateTasksDto(string Headline, string Note, DateTime DeadLine);
