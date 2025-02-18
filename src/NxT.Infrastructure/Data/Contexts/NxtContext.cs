using NxT.Core.Models;
using Microsoft.EntityFrameworkCore;
using NxT.Core.Contracts;

namespace NxT.Infrastructure.Data.Contexts;

public class NxtContext
    (DbContextOptions<NxtContext> options) : DbContext(options)
{
    public DbSet<Department> Departments { get; init; } = default!;
    public DbSet<Seller> Sellers { get; init; } = default!;
    public DbSet<SalesRecord> SalesRecords { get; init; } = default!;
    public DbSet<Commission> Commissions { get; init; } = default!;
    public DbSet<CommissionPerSale> CommissionPerSale { get; init; } = default!;
    public DbSet<CommissionPerGoal> CommissionPerGoal { get; init; } = default!;
    public DbSet<TieredCommission> TieredCommission { get; init; } = default!;
    public DbSet<TierRange> Range { get; init; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(NxtContext).Assembly);
}