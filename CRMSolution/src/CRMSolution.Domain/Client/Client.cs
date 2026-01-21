namespace CRMSolution.Domain.Client;

public class Client
{

    
    public Client() { }


    public Client(
        string _clientId,
        string _name,
        string _famaly,
        string _otchestvo,
        string _mail,
        string _numberOfPhone,
        int _numbersOfReqest,
        string _passpor_t)
    {
        _name = Name;
        _famaly = Famaly;
        _otchestvo = Otchestvo;
        _mail = Mail;
        _numberOfPhone = NumberOfPhone;
        _numbersOfReqest = NumbersOfReqest;
        _passpor_t = Passport;
    }

    public required Guid ClientId { get; set; }

    public required string Name { get; set; } = string.Empty;

    public required string Famaly { get; set; } = string.Empty;

    public string? Otchestvo { get; set; } = string.Empty;

    public string Mail { get; set; } = string.Empty;

    public string NumberOfPhone { get; set; } = string.Empty;

    public int NumbersOfReqest { get; set; } // Сколько раз обращался до этого

    public required string Passport { get; set; } // Паспортные данные

}




