using System.Data;

namespace CRMSolution.Application;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}