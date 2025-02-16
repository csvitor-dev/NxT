using NxT.Core.Models;

namespace NxT.Core.Contracts;

public class TieredCommissionStrategy(decimal[] ranges, decimal[] commissionRates) : ICommissionStrategy
{
    public decimal Calculate(Seller seller)
    {
        var end = DateTime.Now;
        var start = end.AddDays(-30);
        var totalSales = (from sale in seller.Sales
            where sale.Date >= start && sale.Date <= end
            select sale.Amount).Sum();

        var totalCommission = CalculatePerCommission(totalSales);

        return seller.BaseSalary + totalCommission;
    }

    private decimal CalculatePerCommission(decimal amount)
    {
        var commission = 0.0m;
        var remainder = amount;

        for (var i = 0; i < ranges.Length; i++)
            if (remainder > 0)
            {
                var valueInRange = Math.Min(remainder, ranges[i]);
                commission += valueInRange * commissionRates[i];
                
                remainder -= valueInRange;
            }

        return commission;
    }
}