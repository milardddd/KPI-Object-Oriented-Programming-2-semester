namespace BankSystem;

public sealed class AccountCashOperation
{
    public AccountCashOperation(decimal amount, DateTime date, string note)
    {
        this.Amount = amount;
        this.Date = date;
        this.Note = note ?? string.Empty;
    }

    public decimal Amount { get; }

    public DateTime Date { get; }

    public string Note { get; }

    public override string ToString()
    {
        string status = this.Amount < 0 ? "Debited from account" : "Credited to account";
        return $"{this.Date:MM'/'dd'/'yyyy HH':'mm':'ss} {this.Note} : {status} {this.Amount}.";
    }
}
