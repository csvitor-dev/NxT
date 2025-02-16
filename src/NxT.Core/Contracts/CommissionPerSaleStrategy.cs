using NxT.Core.Models;

namespace NxT.Core.Contracts;

public class CommissionPerSaleStrategy(decimal commissionRate) : ICommissionStrategy
{
    public decimal Calculate(Seller seller)
    {
        var end = DateTime.Now;
        var start = end.AddDays(-30);

        var salesOnPeriod = seller.TotalSales(start, end);

        return seller.BaseSalary + commissionRate * salesOnPeriod;
    }
}