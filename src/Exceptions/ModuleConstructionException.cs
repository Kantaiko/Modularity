using System.ComponentModel;
using Kantaiko.Modularity.Resources;

namespace Kantaiko.Modularity.Exceptions;

/// <summary>
/// Represents an error occurred while constructing a module.
/// </summary>
public class ModuleConstructionException : Exception
{
    internal ModuleConstructionException(Type moduleType, [Localizable(true)] string message) :
        base(string.Format(Strings.UnableToConstructModule, moduleType.FullName, message)) { }
}
