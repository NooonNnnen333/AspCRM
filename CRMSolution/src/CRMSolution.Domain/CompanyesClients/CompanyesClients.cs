namespace CRMSolution.Domain.CompanyesClients;

public class CompanyesClients
{
    public required Guid CompanyesClientsId { get; set; }
    
    public string Mail { get; set; } = string.Empty; 
    public string NumberOfPhone { get; set; } = string.Empty;
    public string Inn { get; set; } = string.Empty;
    
}