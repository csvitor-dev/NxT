namespace NxT.Core.Contracts;

public class TierRange
{
    public int ID { get; set; }
    public decimal Amount { get; set; }
    public decimal CommissionRate { get; set; }
    public int TierID { get; set; }

    public TierRange() { }
    public TierRange(int id, decimal amount, decimal commissionRate, int tierId)
    {
        ID = id;
        Amount = amount;
        CommissionRate = commissionRate;
        TierID = tierId;
    }
}