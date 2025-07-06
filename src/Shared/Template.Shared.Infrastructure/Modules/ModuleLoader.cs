using System.Reflection;

namespace Template.Shared.Infrastructure.Modules;

public static class ModuleLoader
{
    public static IList<Assembly> LoadAssemblies()
    {
        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .ToList();

        var locations = assemblies
            .Where(assembly => !assembly.IsDynamic)
            .Select(assembly => assembly.Location)
            .ToList();

        var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(file => !locations.Contains(file, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        files.ForEach(file => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(file))));

        return assemblies;
    }

    public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
    {
        var modules = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type is { IsInterface: false } &&
                           type.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .ToList();

        return modules;
    }
}