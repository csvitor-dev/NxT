namespace NxT.Core.ValueObjects.NiD;

internal abstract class NiDGenerator : UtilitiesBase
{
    internal static string Generate(int? seed = null)
    {
        Random r = new();
        var x1 = FirstUnit(seed) ?? r.Next(0, 256);
        var (x2, x3) = (r.Next(0, 256), r.Next(0, 256));

        var alpha = Mod(x3 - x1, 10);
        var beta = Mod(x2 - x1, 10);
        var gamma = Mod(x1 - (alpha + beta), 10);

        return $"{x1:X2}{x2:X2}{x3:X2}-{alpha}{beta}{gamma}";
    }

    private static int? FirstUnit(int? seed)
        => seed.HasValue ? Mod(seed.Value, 256) : null;
}