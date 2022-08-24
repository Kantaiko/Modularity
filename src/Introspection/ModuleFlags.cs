namespace Kantaiko.Modularity.Introspection;

/// <summary>
/// The mostly used flags of the module.
/// </summary>
[Flags]
public enum ModuleFlags
{
    /// <summary>
    /// The module is a regular module.
    /// </summary>
    None = 0,

    /// <summary>
    /// The module represents a library that is intended to be used by other modules.
    /// </summary>
    Library = 1 << 0,

    /// <summary>
    /// The module should be hidden from the admin interfaces.
    /// </summary>
    Hidden = 1 << 1
}
