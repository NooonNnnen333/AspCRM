namespace CRMSolution.Domain.Emploees;

public class Employees
{
    protected Employees()
    {
    }

    public Employees(Guid _employeesId,
        string _Name,
        string _Famaly,
        string? _Otchestvo,
        string _Mail,
        Role _Role,
        string _NumberOfPhone)
    {
        EmployeesId = _employeesId;
        Name = _Name;
        Famaly = _Famaly;
        Otchestvo = _Otchestvo;
        Mail = _Mail;
        Role = _Role;
        NumberOfPhone = _NumberOfPhone;
    }

    public required Guid EmployeesId { get; set; }

    public required string Name { get; set; } = string.Empty;

    public required string Famaly { get; set; } = string.Empty;

    public string? Otchestvo { get; set; } = string.Empty;

    public string Mail { get; set; } = string.Empty;

    public Role Role { get; set; }

    public string NumberOfPhone { get; set; } = string.Empty;

}

public enum Role
{
    Admin,
    Manager,
    SaleHead,
    Analitics,
    Legal
}