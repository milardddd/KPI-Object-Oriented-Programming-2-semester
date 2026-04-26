using System;
using System.Collections.Generic;

namespace WorkforceStructure;

public class Company
{
    private readonly IList<Employee> employees;

    public Company(IList<Employee> employees)
    {
        this.employees = employees ?? throw new ArgumentNullException(nameof(employees));
    }

    public void DistributeBonuses(decimal companyBonus)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(companyBonus);

        foreach (var employee in this.employees)
        {
            employee.AssignBonus(companyBonus);
        }
    }

    public decimal CalculateTotalPayroll()
    {
        decimal total = 0;
        foreach (var employee in this.employees)
        {
            total += employee.CalculateTotalPay();
        }

        return total;
    }

    public string GetHighestPaidEmployeeName()
    {
        if (this.employees == null || this.employees.Count == 0)
        {
            throw new InvalidOperationException();
        }

        Employee highestPaid = this.employees[0];
        foreach (var employee in this.employees)
        {
            if (employee.Salary > highestPaid.Salary)
            {
                highestPaid = employee;
            }
        }

        return highestPaid.Name;
    }
}
