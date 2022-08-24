using Kantaiko.Modularity.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Kantaiko.Modularity;

/// <summary>
/// The interface which must be implemented by any module class.
/// </summary>
public interface IModule
{
    /// <summary>
    /// Constructs and returns the metadata of the module.
    /// <br/>
    /// This method is guaranteed to be called only once for each module even if the module was added multiple times.
    /// </summary>
    /// <returns>The metadata about the module.</returns>
    ModuleMetadata GetMetadata();

    /// <summary>
    /// Configures the service container with the module-specific services.
    /// <br/>
    /// This method is guaranteed to be called only once for each module even if the module was added multiple times.
    /// </summary>
    /// <param name="services">A service collection to add services to.</param>
    /// <param name="metadata">A metadata of the module.</param>
    void ConfigureServices(IServiceCollection services, ModuleMetadata metadata);
}
