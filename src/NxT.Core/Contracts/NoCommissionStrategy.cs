using NxT.Core.Models;

namespace NxT.Core.Contracts;

public class NoCommissionStrategy : ICommissionStrategy
{
    public decimal Calculate(Seller seller)
        => seller.BaseSalary;
}