using NxT.Core.Models;

namespace NxT.Core.Contracts;

public class CommissionPerGoalStrategy(decimal goal, decimal commissionRate, decimal extraRate) : ICommissionStrategy
{
    public decimal Calculate(Seller seller)
    {
        var end = DateTime.Now;
        var start = end.AddDays(-30);

        var sales = seller.TotalSales(start, end);

        if (sales < goal) 
            return seller.BaseSalary;

        var surplus = sales - goal;
        var commission = goal * commissionRate + surplus * extraRate;

        return commission;
    }
}