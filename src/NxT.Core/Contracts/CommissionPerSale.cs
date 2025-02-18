namespace NxT.Core.Contracts;

public class CommissionPerSale : Commission
{
    public CommissionPerSale() { }
    public CommissionPerSale(int id, decimal baseRate) 
        : base(id, baseRate) { }

    protected sealed override decimal CalculateCommission(decimal sales) 
        => BaseRate * sales;
}