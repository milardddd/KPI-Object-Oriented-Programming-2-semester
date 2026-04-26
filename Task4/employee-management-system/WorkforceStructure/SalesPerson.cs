using System;

namespace WorkforceStructure;

public class SalesPerson : Employee
{
    private readonly int salesPercentage;

    public SalesPerson(string name, decimal salary, int salesPercentage)
        : base(name, salary)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(salesPercentage);
        this.salesPercentage = salesPercentage;
    }

    public override void AssignBonus(decimal bonus)
    {
        decimal factor = 1;

        if (this.salesPercentage > 200)
        {
            factor = 3;
        }
        else if (this.salesPercentage > 100)
        {
            factor = 2;
        }

        base.AssignBonus(bonus * factor);
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Sales Percentage: {this.salesPercentage}%";
    }
}
