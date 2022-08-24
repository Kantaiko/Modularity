using Kantaiko.Modularity.Introspection;
using Kantaiko.Properties;
using Kantaiko.Properties.Immutable;

namespace Kantaiko.Modularity.Metadata;

/// <summary>
/// The metadata of the module.
/// </summary>
public class ModuleMetadata
{
    /// <summary>
    /// Creates a new instance of the <see cref="ModuleMetadata"/> class with required parameters.
    /// </summary>
    /// <param name="displayName">A display name of the module.</param>
    /// <param name="version">A version of the module.</param>
    public ModuleMetadata(string displayName, Version version)
    {
        ArgumentNullException.ThrowIfNull(displayName);
        ArgumentNullException.ThrowIfNull(version);

        DisplayName = displayName;
        Version = version;
    }

    /// <summary>
    /// The display name of the module.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// The version of the module.
    /// </summary>
    public Version Version { get; }

    /// <summary>
    /// The description of the module.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// The flags of the module.
    /// </summary>
    public ModuleFlags Flags { get; init; } = ModuleFlags.None;

    /// <summary>
    /// The user-defined properties of the module.
    /// </summary>
    public IReadOnlyPropertyCollection Properties { get; init; } = ImmutablePropertyCollection.Empty;
}
