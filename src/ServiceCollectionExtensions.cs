using Kantaiko.Modularity.Internal;
using Kantaiko.Modularity.Introspection;
using Kantaiko.Modularity.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kantaiko.Modularity;

/// <summary>
/// The modularity extensions for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Gets the configuration associated with the current application.
    /// </summary>
    /// <param name="services">A <see cref="IServiceCollection"/> instance.</param>
    /// <returns>The configuration associated with the current application.</returns>
    public static IConfiguration GetConfiguration(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return ServiceCollectionHelper.GetHostBuilderContext(services).Configuration;
    }

    /// <summary>
    /// Gets the <see cref="IHostEnvironment"/> associated with the current application.
    /// </summary>
    /// <param name="services">A <see cref="IServiceCollection"/> instance.</param>
    /// <returns>The <see cref="IHostEnvironment"/> associated with the current application.</returns>
    public static IHostEnvironment GetHostingEnvironment(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return ServiceCollectionHelper.GetHostBuilderContext(services).HostingEnvironment;
    }

    /// <summary>
    /// Adds the modularity services which make the <see cref="HostInfo"/>
    /// service available event no modules will be registered.
    /// <br/>
    /// If it's guaranteed that at least one module will be registered, this method is not necessary.
    /// </summary>
    /// <param name="services">A <see cref="IServiceCollection"/> instance.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddModularity(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        ServiceCollectionHelper.TryAddHostInfo(services);

        return services;
    }

    /// <summary>
    /// Registers the module and their services in the container.
    /// <br/>
    /// It's guaranteed that the services of the module will be registered only once.
    /// <br/>
    /// If this method is called inside a configuration method of other module,
    /// the specified module will be marked as a dependency of the current module.
    /// </summary>
    /// <param name="services">A <see cref="IServiceCollection"/> instance.</param>
    /// <typeparam name="TModule">The type of the module to register.</typeparam>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddModule<TModule>(this IServiceCollection services) where TModule : IModule
    {
        ArgumentNullException.ThrowIfNull(services);

        ServiceCollectionHelper.GetModuleManager(services).AddModule(typeof(TModule));

        return services;
    }

    /// <summary>
    /// Registers the module and their services in the container.
    /// <br/>
    /// It's guaranteed that the services of the module will be registered only once.
    /// <br/>
    /// If this method is called inside a configuration method of other module,
    /// the specified module will be marked as a dependency of the current module.
    /// </summary>
    /// <param name="services">A <see cref="IServiceCollection"/> instance.</param>
    /// <param name="moduleType">A type of the module to register.</param>
    /// <exception cref="ArgumentException">The <paramref name="moduleType"/> is not a valid module type.</exception>
    public static void AddModule(this IServiceCollection services, Type moduleType)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(moduleType);

        if (!moduleType.IsAssignableTo(typeof(IModule)))
        {
            throw new ArgumentException(string.Format(Strings.InvalidModuleType, moduleType.FullName),
                nameof(moduleType));
        }

        ServiceCollectionHelper.GetModuleManager(services).AddModule(moduleType);
    }

    /// <summary>
    /// Checks if the specified module is registered in the container.
    /// </summary>
    /// <param name="services">A <see cref="IServiceCollection"/> instance.</param>
    /// <typeparam name="TModule">The type of the module to check.</typeparam>
    /// <returns>True if the module is registered in the container; otherwise, false.</returns>
    public static bool IsModuleRegistered<TModule>(this IServiceCollection services) where TModule : IModule
    {
        ArgumentNullException.ThrowIfNull(services);

        return ServiceCollectionHelper.GetModuleManager(services).IsRegistered(typeof(TModule));
    }

    /// <summary>
    /// Checks if the specified module is registered in the container.
    /// </summary>
    /// <param name="services">A <see cref="IServiceCollection"/> instance.</param>
    /// <param name="moduleType">A type of the module to check.</param>
    /// <returns>True if the module is registered in the container; otherwise, false.</returns>
    /// <exception cref="ArgumentException">The <paramref name="moduleType"/> is not a valid module type.</exception>
    public static bool IsModuleRegistered(this IServiceCollection services, Type moduleType)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(moduleType);

        if (!moduleType.IsAssignableTo(typeof(IModule)))
        {
            throw new ArgumentException(string.Format(Strings.InvalidModuleType, moduleType.FullName),
                nameof(moduleType));
        }

        return ServiceCollectionHelper.GetModuleManager(services).IsRegistered(moduleType);
    }
}
