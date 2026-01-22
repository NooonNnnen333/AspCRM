using CRMSolution.Domain.CompanyesClients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSolution.Infrastructure.Postgres.Configuration;

public class CompanyesClientsConfiguration : IEntityTypeConfiguration<CompanyesClients>
{
    public void Configure(EntityTypeBuilder<CompanyesClients> builder)
    {
        builder.ToTable("companyes_clients");
        builder.HasKey(x => x.CompanyesClientsId);
    }
}
