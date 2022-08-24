using System.Collections.Immutable;
using System.Reflection;

namespace Kantaiko.Modularity.Introspection;

/// <summary>
/// Represents the information about the host containing modules.
/// </summary>
public class HostInfo
{
    internal HostInfo()
    {
        Modules = ImmutableArray<ModuleInfo>.Empty;
        ExplicitModules = ImmutableArray<ModuleInfo>.Empty;
        Assemblies = ImmutableHashSet<Assembly>.Empty;
    }

    internal HostInfo(IReadOnlyList<ModuleInfo> modules,
        IReadOnlyList<ModuleInfo> explicitModules,
        IReadOnlySet<Assembly> assemblies)
    {
        Modules = modules;
        ExplicitModules = explicitModules;
        Assemblies = assemblies;
    }

    /// <summary>
    /// The list of all modules loaded by the host in order of resolution.
    /// </summary>
    public IReadOnlyList<ModuleInfo> Modules { get; }

    /// <summary>
    /// The list of modules loaded only explicitly by the host in order of request.
    /// </summary>
    public IReadOnlyList<ModuleInfo> ExplicitModules { get; }

    /// <summary>
    /// The set of assemblies associated with loaded modules.
    /// </summary>
    public IReadOnlySet<Assembly> Assemblies { get; }
}
