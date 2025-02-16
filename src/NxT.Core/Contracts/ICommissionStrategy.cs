using NxT.Core.Models;

namespace NxT.Core.Contracts;

public interface ICommissionStrategy
{
    public decimal Calculate(Seller seller);
}