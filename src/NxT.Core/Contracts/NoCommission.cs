using NxT.Core.Models;

namespace NxT.Core.Contracts;

public class NoCommission : Commission
{
    protected sealed override decimal CalculateCommission(decimal sales)
        => 0.0m;
}