using CRMSolution.Domain.Task;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSolution.Infrastructure.Postgres.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<TaskC>
{
    public void Configure(EntityTypeBuilder<TaskC> builder)
    {
        builder.HasKey(x => x.TaskId);

        builder.Property(x => x.EmloyeesId);

        builder.Property(x => x.ClientId);

        builder.Property(x => x.ProductId);

        builder.Property(x => x.Title)
            .IsRequired();

        builder.Property(x => x.DateOfCreatedThis);

        builder.Property(x => x.DateOfPlaneDo);

        builder.Property(x => x.Note);

        builder.Property(x => x.StatusTask);

    }
}