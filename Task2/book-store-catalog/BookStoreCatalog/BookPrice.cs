using System.Globalization;

namespace BookStoreCatalog;

public class BookPrice
{
    private decimal amount;
    private string currency;

    public BookPrice()
        : this(0m, "USD")
    {
    }

    public BookPrice(decimal amount, string currency)
    {
        ThrowExceptionIfAmountIsNotValid(amount, nameof(amount));
        ThrowExceptionIfCurrencyIsNotValid(currency, nameof(currency));

        this.amount = amount;
        this.currency = currency;
    }

    public decimal Amount
    {
        get => this.amount;
        set
        {
            ThrowExceptionIfAmountIsNotValid(value, nameof(value));
            this.amount = value;
        }
    }

    public string Currency
    {
        get => this.currency;
        set
        {
            ThrowExceptionIfCurrencyIsNotValid(value, nameof(value));
            this.currency = value;
        }
    }

    public override string ToString()
    {
        return string.Format(CultureInfo.InvariantCulture, "{0:N2} {1}", this.amount, this.currency);
    }

    private static void ThrowExceptionIfAmountIsNotValid(decimal amount, string paramName)
    {
        if (amount < 0m)
        {
            throw new ArgumentException("Amount must be greater than or equal to zero.", paramName);
        }
    }

    private static void ThrowExceptionIfCurrencyIsNotValid(string currency, string paramName)
    {
        if (currency is null)
        {
            throw new ArgumentNullException(paramName);
        }

        if (currency.Length != 3)
        {
            throw new ArgumentException("Currency must have 3 characters.", paramName);
        }

        foreach (char ch in currency)
        {
            if (!char.IsLetter(ch))
            {
                throw new ArgumentException("Currency must contain only letters.", paramName);
            }
        }
    }
}
