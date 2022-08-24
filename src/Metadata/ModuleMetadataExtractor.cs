using System.Reflection;
using Kantaiko.Modularity.Introspection;
using Kantaiko.Modularity.Resources;

namespace Kantaiko.Modularity.Metadata;

/// <summary>
/// The helper class used to extract the module metadata from the class and its attributes.
/// </summary>
public static class ModuleMetadataExtractor
{
    /// <summary>
    /// Extracts the module metadata from the module type.
    /// <br/>
    /// If the module class applies the <see cref="ModuleAttribute"/>, the metadata is extracted from the attribute.
    /// </summary>
    /// <param name="moduleType">A type that represents a module.</param>
    /// <returns>The metadata of the module.</returns>
    /// <exception cref="ArgumentException">The <paramref name="moduleType"/> is not a valid module type.</exception>
    public static ModuleMetadata Extract(Type moduleType)
    {
        ArgumentNullException.ThrowIfNull(moduleType);

        if (!moduleType.IsAssignableTo(typeof(IModule)))
        {
            throw new ArgumentException(string.Format(Strings.InvalidModuleType, moduleType.FullName),
                nameof(moduleType));
        }

        var moduleAttribute = moduleType
            .GetCustomAttributes()
            .OfType<ModuleAttribute>()
            .FirstOrDefault();

        string? displayName = null;
        Version? version = null;
        string? description = null;
        ModuleFlags flags = default;

        if (moduleAttribute is not null)
        {
            displayName = moduleAttribute.DisplayName;
            description = moduleAttribute.Description;
            flags = moduleAttribute.Flags;

            if (moduleAttribute.Version is not null)
            {
                version = Version.Parse(moduleAttribute.Version);
            }
        }

        displayName ??= NormalizeModuleName(moduleType.Name);
        version ??= moduleType.Assembly.GetName().Version ?? new Version();

        return new ModuleMetadata(displayName, version)
        {
            Description = description,
            Flags = flags
        };
    }

    private static string NormalizeModuleName(string moduleName)
    {
        if (!moduleName.EndsWith("Module")) return moduleName;

        var modulePostfixIndex = moduleName.LastIndexOf("Module", StringComparison.Ordinal);
        return moduleName.Substring(0, modulePostfixIndex);
    }
}
