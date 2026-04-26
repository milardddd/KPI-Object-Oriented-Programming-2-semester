using System;

namespace WorkforceStructure;

public class Manager : Employee
{
    private readonly int clientCount;

    public Manager(string name, decimal salary, int clientCount)
        : base(name, salary)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(clientCount);
        this.clientCount = clientCount;
    }

    public override void AssignBonus(decimal bonus)
    {
        decimal totalBonus = bonus;

        if (this.clientCount > 150)
        {
            totalBonus += 1000;
        }
        else if (this.clientCount > 100)
        {
            totalBonus += 500;
        }

        base.AssignBonus(totalBonus);
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Clients: {this.clientCount}";
    }
}
