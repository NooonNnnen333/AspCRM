namespace CRMSolution.Domain.Client;

public class Client
{
    public required Guid ClientId { get; set; }

    public required string Name { get; set; } = string.Empty;
    public required string Famaly { get; set; } = string.Empty;
    public string? Otchestvo { get; set; } = string.Empty;
    
    public string Mail { get; set; } = string.Empty; 
    public string NumberOfPhone { get; set; } = string.Empty;
    public int NumbersOfReqest { get; set; } // Сколько раз обращался до этого
    public required string Passport { get; set; } // Паспортные данные

}




