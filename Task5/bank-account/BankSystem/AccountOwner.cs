using System.Collections.ObjectModel;
using BankSystem.Accounts;
using BankSystem.Helpers;

namespace BankSystem;

public sealed class AccountOwner
{
    private readonly List<BankAccount> accounts = [];

    public AccountOwner(string? firstName, string? lastName, string? email)
    {
        VerifyString(firstName, nameof(firstName));
        VerifyString(lastName, nameof(lastName));
        VerifyString(email, nameof(email));

        if (!email!.IsEmailValid())
        {
            throw new ArgumentException("Invalid email format.", nameof(email));
        }

        this.FirstName = firstName!;
        this.LastName = lastName!;
        this.Email = email!;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public string Email { get; }

    public void Add(BankAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        this.accounts.Add(account);
    }

    public ReadOnlyCollection<BankAccount> Accounts() => this.accounts.AsReadOnly();

    public override string ToString() => $"{this.FirstName} {this.LastName}, {this.Email}.";

    private static void VerifyString(string? value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("String value cannot be null, empty, or whitespace.", parameterName);
        }
    }
}
