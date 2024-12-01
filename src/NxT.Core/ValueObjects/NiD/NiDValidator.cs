using System.Text.RegularExpressions;

namespace NxT.Core.ValueObjects.NiD;

internal abstract class NiDValidator : UtilitiesBase
{
    private static readonly Regex NiDPattern = new("^[0-9A-Fa-f]{6}-\\d{3}$", RegexOptions.Compiled);
    internal static void Validate(string nidValue)
    {
        if (IsValidFormat(nidValue) is false)
            throw new ArgumentException("Invalid NiD format");
        
        if (IsValidNiD(nidValue) is false)
            throw new ArgumentException("Invalid NiD checksum");
    }

    private static bool IsValidFormat(string value)
        => NiDPattern.IsMatch(value);

    private static bool IsValidNiD(string value)
    {
        var parts = value.Split('-');
        var (hex, numbers) = (parts[0], parts[1]);

        if (hex.Length != 6 || numbers.Length != 3)
            return false;
        var x1 = Convert.ToInt32(hex[..2], 16);
        var x2 = Convert.ToInt32(hex[2..4], 16);
        var x3 = Convert.ToInt32(hex[4..6], 16);

        var alpha = numbers[0] - '0';
        var beta = numbers[1] - '0';
        var gamma = numbers[2] - '0';
        
        return alpha == Mod(x3 - x1, 10) &&
               beta == Mod(x2 - x1, 10) &&
               gamma == Mod(x1 - (alpha + beta), 10);
    }
}