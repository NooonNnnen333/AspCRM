namespace CRMSolution.Domain.Emploees;

public class Employees
{
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