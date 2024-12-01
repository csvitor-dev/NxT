namespace NxT.Core.ValueObjects.NiD;

internal abstract class UtilitiesBase
{
    internal static int Mod(int value, int m)
        => (value % m + m) % m;
}