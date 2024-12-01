namespace NxT.Core.ValueObjects.NiD;

public sealed record NxtIdentifier
{
    private readonly string _identifier;
    
    private NxtIdentifier(int? seed = null)
        => _identifier = NiDGenerator.Generate(seed);

    public static NxtIdentifier Create(int? seed = null)
        => new(seed);

    public static void Validate(NxtIdentifier nid) 
        => NiDValidator.Validate(nid._identifier);

    public override string ToString()
        => $"NiD: {_identifier}";
}