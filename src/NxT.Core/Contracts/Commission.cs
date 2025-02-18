using NxT.Core.Models;

namespace NxT.Core.Contracts;

public abstract class Commission
{
    public int ID { get; set; }

    public Commission() { }

    public Commission(int id)
     => ID = id;

    protected abstract decimal CalculateCommission(decimal sales);
    
    public decimal Calculate(Seller seller)
    {
        var (start, end) = CommissionPeriod();
        var salesOnPeriod = (from sale in seller.Sales
            where sale.Date >= start && sale.Date <= end
            select sale.Amount).Sum();

        var commission = CalculateCommission(salesOnPeriod);

        return commission;
    }

    private (DateTime, DateTime) CommissionPeriod()
    {
        var now = DateTime.Now;

        var start = new DateTime(now.Year, now.Month, 1);
        var end = new DateTime(now.Year, now.Month, 
            DateTime.DaysInMonth(now.Year, now.Month));

        return (start, end);
    }

}