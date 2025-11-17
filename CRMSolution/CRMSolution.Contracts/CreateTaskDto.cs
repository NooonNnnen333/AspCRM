using CRMSolution.Domain.Task;

namespace CRMSolution.Presenters;

public record CreateTaskDto(string Headline, string Note, DateTime DeadLine, Guid Clientid, Guid Emploees);

public record GetTasksDto(string Headline, Status status, DateTime DeadLine);

public record UpdateTasksDto(string Headline, string Note, DateTime DeadLine);