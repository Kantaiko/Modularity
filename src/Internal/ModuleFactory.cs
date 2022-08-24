using Kantaiko.Modularity.Exceptions;
using Kantaiko.Modularity.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Kantaiko.Modularity.Internal;

internal class ModuleFactory
{
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _hostEnvironment;

    public ModuleFactory(IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
    }

    public IModule ConstructModuleInstance(Type type)
    {
        var constructors = type.GetConstructors();

        switch (constructors.Length)
        {
            case 0:
                throw new ModuleConstructionException(type, Strings.ModuleMustContainAccessibleConstructor);
            case > 1:
                throw new ModuleConstructionException(type, Strings.ModuleCannotContainMultipleConstructors);
        }

        var constructor = constructors[0];
        var constructorParameters = new List<object>();

        foreach (var parameterInfo in constructor.GetParameters())
        {
            if (parameterInfo.ParameterType == typeof(IConfiguration))
            {
                constructorParameters.Add(_configuration);
                continue;
            }

            if (parameterInfo.ParameterType == typeof(IHostEnvironment))
            {
                constructorParameters.Add(_hostEnvironment);
                continue;
            }

            throw new ModuleConstructionException(type, Strings.InvalidModuleParameter);
        }

        return (IModule) constructor.Invoke(constructorParameters.ToArray());
    }
}
