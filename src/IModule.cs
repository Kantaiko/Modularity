using Microsoft.Extensions.DependencyInjection;

namespace Kantaiko.Modularity;

public interface IModule
{
    ModuleMetadata GetMetadata();

    void ConfigureServices(IServiceCollection services, ModuleMetadata metadata);
}
