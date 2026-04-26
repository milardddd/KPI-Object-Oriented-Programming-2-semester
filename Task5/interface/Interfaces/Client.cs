using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Interfaces;

public class Client : IEnumerable<Deposit>
{
    private readonly Deposit[] deposits;

    public Client()
    {
        this.deposits = new Deposit[10];
    }

    public bool AddDeposit(Deposit deposit)
    {
        for (int i = 0; i < this.deposits.Length; i++)
        {
            if (this.deposits[i] == null)
            {
                this.deposits[i] = deposit;
                return true;
            }
        }

        return false;
    }

    public decimal TotalIncome() =>
        this.deposits.Where(d => d != null).Sum(d => d.Income());

    public decimal MaxIncome() =>
        this.deposits.Where(d => d != null).Select(d => d.Income()).DefaultIfEmpty(0m).Max();

    public decimal GetIncomeByNumber(int number)
    {
        int index = number - 1;
        if (index < 0 || index >= 10 || this.deposits[index] == null)
        {
            return 0m;
        }

        return this.deposits[index].Income();
    }

    public void SortDeposits()
    {
        var sorted = this.deposits
            .Where(d => d != null)
            .OrderByDescending(d => d.Amount + d.Income())
            .ToArray();

        Array.Clear(this.deposits, 0, this.deposits.Length);
        for (int i = 0; i < sorted.Length; i++)
        {
            this.deposits[i] = sorted[i];
        }
    }

    public int CountPossibleToProlongDeposit()
    {
        int count = 0;
        foreach (var d in this.deposits)
        {
            if (d is IProlongable p && p.CanToProlong())
            {
                count++;
            }
        }

        return count;
    }

    public IEnumerator<Deposit> GetEnumerator() =>
        this.deposits.Where(d => d != null).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}