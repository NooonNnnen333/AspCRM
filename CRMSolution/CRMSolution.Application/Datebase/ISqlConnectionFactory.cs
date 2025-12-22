using System.Data;

namespace CRMSolution.Infrastructure.Postgres;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}