using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace BankSystem.Helpers;

internal static class CryptoHelper
{
    public static string GenerateHash(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentNullException(nameof(input));
        }

        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        var hashStringBuilder = new StringBuilder(32);

        for (var i = 0; i < 16; i++)
        {
            _ = hashStringBuilder.Append(hashBytes[i].ToString("x2", CultureInfo.InvariantCulture));
        }

        return hashStringBuilder.ToString();
    }
}
