namespace DentallApp.HostApplication.PluginConfiguration;

public static class PluginLoader
{
    private readonly static IEnumerable<string> s_assemblyFiles;
    private readonly static Dictionary<string, Assembly> s_assemblies = new();
    public static IEnumerable<Assembly> Assemblies => s_assemblies.Values;

    static PluginLoader()
    {
        var recoveredValue = EnvReader.Instance["PLUGINS"];
        s_assemblyFiles = recoveredValue
            .Split(" ")
            .Where(pluginFile => !string.IsNullOrWhiteSpace(pluginFile))
            .Select(GetPluginPath);
    }

    private static string GetPluginPath(string pluginFile)
    {
        bool isNotPlugin = !Path.GetExtension(pluginFile).Equals(".dll");
        if (isNotPlugin)
        {
            var message =
            $"""
            '{pluginFile}' plug-in must have the extension .dll
            Please check your configuration file.
            """;
            throw new ArgumentException(message, nameof(pluginFile));
        }

        // Example: DentallApp.ChatBot
        var pluginDirectory = Path.GetFileNameWithoutExtension(pluginFile);
        // Example: /home/admin/DentallApp/src/HostApplication/bin/Debug/net7.0/plugins/DentallApp.ChatBot
        var basePath = Path.Combine(AppContext.BaseDirectory, "plugins", pluginDirectory);
        // Example: /home/admin/DentallApp/src/HostApplication/bin/Debug/net7.0/plugins/DentallApp.ChatBot/DentallApp.ChatBot.dll
        var pluginPath = Path.Combine(basePath, pluginFile);
        return pluginPath;
    }

    /// <summary>
    /// Loads the plugins together with the specified contract.
    /// </summary>
    /// <typeparam name="TContract">
    /// The type of contract shared between the host application and the plugins.
    /// </typeparam>
    /// <returns>
    /// An instance of type <see cref="IEnumerable{TContract}"/> that contains the instances
    /// that implement the contract specified by <typeparamref name="TContract"/>.
    /// </returns>
    public static IEnumerable<TContract> Load<TContract>() where TContract : class
    {
        var contracts = new List<TContract>();
        foreach (string assemblyFile in s_assemblyFiles)
        {
            Assembly currentAssembly = FindAssembly(assemblyFile);
            currentAssembly ??= LoadAssembly(assemblyFile);
            var pluginAttributes = currentAssembly.GetCustomAttributes<PluginAttribute>();
            if (!pluginAttributes.Any())
            {
                var message = $"'{currentAssembly.GetName().Name}' plugin does not use the '{nameof(PluginAttribute)}' attribute.";
                throw new InvalidOperationException(message);
            }
            foreach (PluginAttribute pluginAttribute in pluginAttributes)
            {
                Type type = pluginAttribute.PluginType;
                if (typeof(TContract).IsAssignableFrom(type))
                {
                    var contract = (TContract)Activator.CreateInstance(type);
                    contracts.Add(contract);
                }
            }
        }
        return contracts;
    }

    private static Assembly LoadAssembly(string assemblyFile)
    {
        var loadContext = new PluginLoadContext(assemblyFile);
        var assemblyName = AssemblyName.GetAssemblyName(assemblyFile);
        var currentAssembly = loadContext.LoadFromAssemblyName(assemblyName);
        s_assemblies.Add(assemblyFile, currentAssembly);
        return currentAssembly;
    }

    private static Assembly FindAssembly(string assemblyFile)
    {
        s_assemblies.TryGetValue(assemblyFile, out Assembly assembly);
        return assembly;
    }
}
