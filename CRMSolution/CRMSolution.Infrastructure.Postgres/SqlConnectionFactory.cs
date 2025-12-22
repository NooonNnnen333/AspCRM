using System.Data;
using CRMSolution.Application;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CRMSolution.Infrastructure.Postgres;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration Configuration)
    {
        _configuration = Configuration;
    }

    public IDbConnection Create()
    {
        var connection = new NpgsqlConnection(_configuration.GetConnectionString("Datebase"));

        return connection;
    }
}