namespace NxT.Core.Contracts;

public class CommissionPerSale : Commission
{
    public decimal BaseRate { get; set; }

    public CommissionPerSale() { }
    public CommissionPerSale(int id, decimal baseRate) 
        : base(id)
    {
        BaseRate = baseRate;
    }

    protected sealed override decimal CalculateCommission(decimal sales) 
        => BaseRate * sales;
}