using Kantaiko.Modularity.Introspection;

namespace Kantaiko.Modularity;

/// <summary>
/// The attribute that allows to override the information of the module.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ModuleAttribute : Attribute
{
    /// <summary>
    /// The display name of the module.
    /// </summary>
    public string? DisplayName { get; init; }

    /// <summary>
    /// The version of the module.
    /// <br/>
    /// Must be in the format of <c>Major.Minor.Build.Revision</c>.
    /// </summary>
    public string? Version { get; init; }

    /// <summary>
    /// The description of the module.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// The flags of the module.
    /// </summary>
    public ModuleFlags Flags { get; init; }
}
