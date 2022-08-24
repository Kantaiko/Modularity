using Kantaiko.Modularity.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Kantaiko.Modularity;

/// <summary>
/// The base class for modules.
/// </summary>
public abstract class Module : IModule
{
    /// <summary>
    /// Configures the service container with the module-specific services.
    /// <br/>
    /// This method is guaranteed to be called only once even if the module was added multiple times.
    /// </summary>
    /// <param name="services">A service collection to add services to.</param>
    protected virtual void ConfigureServices(IServiceCollection services) { }

    ModuleMetadata IModule.GetMetadata()
    {
        return ModuleMetadataExtractor.Extract(GetType());
    }

    void IModule.ConfigureServices(IServiceCollection services, ModuleMetadata metadata)
    {
        ConfigureServices(services);
    }
}
