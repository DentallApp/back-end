namespace DentallApp.Shared.Plugin.Contracts;

/// <summary>
/// This attribute is required for the plugin loader to create the instance that implements the contract.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
public class PluginAttribute : Attribute
{
    /// <summary>
    /// Gets an instance of type <see cref="Type"/> that implements the contract.
    /// </summary>
    public Type PluginType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PluginAttribute"/> class.
    /// </summary>
    /// <param name="pluginType">
    /// An instance of type <see cref="Type"/> that implements the contract.
    /// </param>
|    /// <exception cref="ArgumentException">
    /// <c>pluginType</c> is not instantiable.
    /// </exception>
    public PluginAttribute(Type pluginType)
    {
        if(!pluginType.IsClass || pluginType.IsAbstract)
        {
            var message = $"{pluginType.Name} must be a class that can be instantiated. It cannot be abstract or an interface.";
            throw new ArgumentException(message, nameof(pluginType));
        }

        PluginType = pluginType;
    }
}
