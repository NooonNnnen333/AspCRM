namespace CRMSolution.Domain.Task;

public class TaskC
{
    public TaskC(
        Guid id,
        List<Guid> emloyeesId,
        Guid productId,
        string title,
        DateTime dateOfPlaneDo)
    {
        TaskId = id;
        EmloyeesId = emloyeesId;
        ProductId = productId;
        Title = title;
        DateOfPlaneDo = dateOfPlaneDo;
        DateOfCreatedThis = DateOnly.FromDateTime(DateTime.UtcNow);
    }

    private TaskC()
    {
        EmloyeesId = new List<Guid>();
    }

    public Guid TaskId { get; set; }

    public List<Guid> EmloyeesId { get; set; } // Id работника(ов), которые будут задействованы в этой задачи

    public Guid ClientId { get; set; } // Id клиента, с которым связана задача

    public Guid ProductId { get; set; } // Id продукта/услуги

    public string Title { get; set; } = string.Empty;

    public DateOnly DateOfCreatedThis { get; set; } // Дата создания

    public DateTime DateOfPlaneDo { get; set; } // Время выполнения / на сколько назначен созвон/встреча

    public string Note { get; set; } = string.Empty;

    public Status Status { get; set; }

}

public enum Status
{
    /// <summary>
    /// Сделано
    /// </summary>
    DONE,

    /// <summary>
    /// Надо сделать
    /// </summary>
    TO_DO,

    /// <summary>
    /// Не скоро
    /// </summary>
    NO_DUE_SOON
}
