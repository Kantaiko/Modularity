using Kantaiko.Modularity.Introspection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kantaiko.Modularity.Tests;

[UsesVerify]
public class IntrospectionTest
{
    [Module(DisplayName = "CustomTitle")]
    private class ModuleC : Module { }

    private class ModuleB : Module
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddModule<ModuleC>();
        }
    }

    private class ModuleA : Module
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddModule<ModuleB>();
        }
    }

    [Fact]
    public async Task ShouldContainValidIntrospectionInfo()
    {
        var hostBuilder = new HostBuilder();
        hostBuilder.ConfigureServices(services => services.AddModule<ModuleA>());

        var host = hostBuilder.Build();
        var hostInfo = host.Services.GetRequiredService<HostInfo>();

        await Verify(hostInfo)
            .IgnoreMembers("Version", "Assemblies", "Dependents", "Assembly")
            .UseDirectory("__snapshots__");
    }
}
