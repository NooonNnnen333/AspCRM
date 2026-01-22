using CRMSolution.Domain.Emploees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSolution.Infrastructure.Postgres.Configuration;

public class EmployeesConfiguration : IEntityTypeConfiguration<Employees>
{
    public void Configure(EntityTypeBuilder<Employees> builder)
    {
        builder.ToTable("employees");
        builder.HasKey(x => x.EmployeesId);
    }
}
