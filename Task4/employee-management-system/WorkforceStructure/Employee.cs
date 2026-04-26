using System;
using System.Diagnostics.CodeAnalysis;

namespace WorkforceStructure;

public class Employee
{
    private readonly string name;
    private decimal salary;
    private decimal bonus;

    [SuppressMessage("Usage", "CA2208:Instantiate argument exceptions correctly", Justification = "Required by automated tests to use 'value' as param name in constructor")]
    public Employee(string name, decimal salary)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null, empty or whitespace.", nameof(name));
        }

        if (salary < 0)
        {
            throw new ArgumentOutOfRangeException("value", "Salary cannot be less than zero.");
        }

        this.name = name;
        this.salary = salary;
    }

    public string Name => this.name;

    public decimal Salary
    {
        get => this.salary;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Salary cannot be less than zero.");
            }

            this.salary = value;
        }
    }

    public virtual void AssignBonus(decimal bonus)
    {
        if (bonus < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bonus), "Bonus cannot be less than zero.");
        }

        this.bonus = bonus;
    }

    public decimal CalculateTotalPay()
    {
        return this.salary + this.bonus;
    }

    public override string ToString()
    {
        return $"{this.name}, Salary: {this.salary:C2}, Bonus: {this.bonus:C2}";
    }
}
