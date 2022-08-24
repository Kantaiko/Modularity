namespace Kantaiko.Modularity.Introspection;

/// <summary>
/// The identifier of the module.
/// </summary>
public record ModuleIdentifier
{
    internal ModuleIdentifier(Type moduleType)
    {
        ModuleType = moduleType;
    }

    /// <summary>
    /// The type of the module class.
    /// </summary>
    public Type ModuleType { get; }
}
