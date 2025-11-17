namespace CRMSolution.Domain.Task;

public class TaskC
{

    required public Guid TaskId { get; set; }
    /*public List<Guid> EmloyeesId { get; set; }
    public Guid ClientId { get; set; }
    public Guid ProductId { get; set; } // Id продукта/услуги*/

    public required string Title { get; set; } = string.Empty;

    public required DateOnly DateOfCreatedThis { get; set; } // Дата создания

    public DateTime DateOfPlaneDo { get; set; } // Время выполнения / на сколько назначен созвон/встреча

    public string Note { get; set; } = string.Empty;

    public Status Status { get; set; }

}

public enum Status
{
    Done, // Сделано
    ToDo, // Надо сделать
    NoDueSoon // Не скоро
}