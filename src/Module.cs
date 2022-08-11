using Microsoft.Extensions.DependencyInjection;

namespace Kantaiko.Modularity;

public abstract class Module : IModule
{
    ModuleMetadata IModule.GetMetadata()
    {
        return ModuleMetadataExtractor.Extract(GetType());
    }

    protected virtual void ConfigureServices(IServiceCollection services) { }

    void IModule.ConfigureServices(IServiceCollection services, ModuleMetadata metadata)
    {
        ConfigureServices(services);
    }
}
