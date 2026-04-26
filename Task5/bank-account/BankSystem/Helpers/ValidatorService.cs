using System.Globalization;
using System.Net.Mail;

namespace BankSystem.Helpers;

public static class ValidatorService
{
    public static bool IsCurrencyValid(this string? currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            return false;
        }

        string normalized = currency.Trim().ToUpperInvariant();

        return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .Select(culture => new RegionInfo(culture.Name).ISOCurrencySymbol)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Any(symbol => string.Equals(symbol, normalized, StringComparison.OrdinalIgnoreCase));
    }

    public static bool IsEmailValid(this string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            _ = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
