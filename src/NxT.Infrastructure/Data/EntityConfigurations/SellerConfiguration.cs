using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NxT.Core.Models;

namespace NxT.Infrastructure.Data.EntityConfigurations;

public class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.HasKey(e => e.ID);

        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
        builder.Property(e => e.BirthDate).IsRequired();
        builder.Property(e => e.BaseSalary).IsRequired();

        builder.HasOne(e => e.Department).WithMany(d => d.Sellers)
        .HasForeignKey(e => e.DepartmentID).IsRequired();
    }
}