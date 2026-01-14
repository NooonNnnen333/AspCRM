using Shared;

namespace CRMSolution.Application.Errors;

public partial class Errors
{
    public static class TaskC
    {
        public static Error ToManyTasks() =>
            Error.Failure("task.o4en.mnogo", "Слишком много задач!");
    }
}