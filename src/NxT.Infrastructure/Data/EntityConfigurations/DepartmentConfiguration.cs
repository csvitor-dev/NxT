using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NxT.Core.Models;

namespace NxT.Infrastructure.Data.EntityConfigurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(e => e.ID);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}