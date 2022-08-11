using System.Reflection;
using Kantaiko.Properties;

namespace Kantaiko.Modularity.Introspection;

public class ModuleInfo : IReadOnlyPropertyContainer
{
    private readonly ModuleMetadata _metadata;

    internal ModuleInfo(ModuleIdentifier id, ModuleMetadata metadata, IReadOnlyList<ModuleInfo> dependencies)
    {
        Id = id;
        _metadata = metadata;
        Dependencies = dependencies;
    }

    /// <summary>
    /// The unique identifier of the module.
    /// </summary>
    public ModuleIdentifier Id { get; }

    /// <summary>
    /// The assembly containing the module.
    /// </summary>
    public Assembly Assembly => Id.ModuleType.Assembly;

    /// <summary>
    /// The display name of the module. Should be used in admin interfaces.
    /// By default, it is a module class name without "Module" suffix.
    /// </summary>
    public string DisplayName => _metadata.DisplayName;

    /// <summary>
    /// The description of the module. Should be used in admin interfaces.
    /// </summary>
    public string? Description => _metadata.Description;

    /// <summary>
    /// Version of the module.
    /// </summary>
    public Version Version => _metadata.Version;

    /// <summary>
    /// Flags, representing some useful information about the module.
    /// </summary>
    public ModuleFlags Flags => _metadata.Flags;

    /// <summary>
    /// Collection of user-defined module properties.
    /// </summary>
    public IReadOnlyPropertyCollection Properties => _metadata.Properties;

    /// <summary>
    /// Dependencies, explicitly requested by the module.
    /// </summary>
    public IReadOnlyList<ModuleInfo> Dependencies { get; }

    /// <summary>
    /// All modules that have requested this module to load.
    /// Empty, if this module was loaded directly by the host.
    /// </summary>
    public IReadOnlyList<ModuleInfo> Dependents { get; internal set; } = null!;
}
