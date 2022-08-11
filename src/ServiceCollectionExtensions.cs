using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kantaiko.Modularity;

public static class ServiceCollectionExtensions
{
    public static IConfiguration GetConfiguration(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return ServiceCollectionHelper.GetHostBuilderContext(services).Configuration;
    }

    public static IHostEnvironment GetHostingEnvironment(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return ServiceCollectionHelper.GetHostBuilderContext(services).HostingEnvironment;
    }

    public static void AddModularity(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        ServiceCollectionHelper.TryAddHostInfo(services);
    }

    public static void AddModule<TModule>(this IServiceCollection services) where TModule : IModule
    {
        ArgumentNullException.ThrowIfNull(services);

        ServiceCollectionHelper.GetModuleManager(services).AddModule(typeof(TModule));
    }

    public static void AddModule(this IServiceCollection services, Type moduleType)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(moduleType);

        if (!moduleType.IsAssignableTo(typeof(IModule)))
        {
            throw new ArgumentException($"Type \"{moduleType.Name}\" is not a valid module type", nameof(moduleType));
        }

        ServiceCollectionHelper.GetModuleManager(services).AddModule(moduleType);
    }

    public static bool IsModuleRegistered<TModule>(this IServiceCollection services) where TModule : IModule
    {
        ArgumentNullException.ThrowIfNull(services);

        return ServiceCollectionHelper.GetModuleManager(services).IsRegistered(typeof(TModule));
    }

    public static bool IsModuleRegistered(this IServiceCollection services, Type moduleType)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(moduleType);

        return ServiceCollectionHelper.GetModuleManager(services).IsRegistered(moduleType);
    }
}
