using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NxT.Core.Models;

namespace NxT.Infrastructure.Data.EntityConfigurations;

public class SalesRecordConfiguration : IEntityTypeConfiguration<SalesRecord>
{
    public void Configure(EntityTypeBuilder<SalesRecord> builder)
    {
        builder.HasKey(e => e.ID);

        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.Amount).IsRequired();
        builder.Property(e => e.Status).IsRequired();

        builder.HasOne(e => e.Seller).WithMany(s => s.Sales)
        .HasForeignKey(e => e.SellerID).IsRequired();
    }
}