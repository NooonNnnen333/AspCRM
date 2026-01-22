namespace CRMSolution.Application;

public interface ITokenService
{
    string CreateToken(string userName);
}
