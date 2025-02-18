namespace NxT.Core.Contracts;

public class TieredCommission : Commission
{
    public IEnumerable<TierRange> Ranges { get; set; }

    public TieredCommission()
        => Ranges = [];
    public TieredCommission(int id, IEnumerable<TierRange> ranges) : base(id)
        => Ranges = ranges;

    protected sealed override decimal CalculateCommission(decimal sales)
    {
        var commission = 0.0m;
        var remainder = sales;

        foreach (var range in Ranges)
            if (remainder > 0)
            {
                var valueInRange = Math.Min(remainder, range.Amount);
                commission += valueInRange * range.CommissionRate;
                
                remainder -= valueInRange;
            }

        return commission;
    }
}