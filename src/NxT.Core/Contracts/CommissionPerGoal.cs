using NxT.Core.Models;

namespace NxT.Core.Contracts;

public class CommissionPerGoal : Commission
{
    public decimal Goal { get; set;}
    public decimal ExtraRate { get; set; }

    public CommissionPerGoal() { }
    public CommissionPerGoal(int id, decimal goal, decimal baseRate, decimal extraRate)
        : base(id, baseRate)
    {
        Goal = goal;
        ExtraRate = extraRate;
    }

    protected sealed override decimal CalculateCommission(decimal sales)
    {
        if (sales < Goal)
            return 0.0m;

        var surplus = sales - Goal;
        var commission = Goal * BaseRate + surplus * ExtraRate;

        return commission;
    }
}