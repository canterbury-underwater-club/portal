using Microsoft.Extensions.Configuration;

namespace CanterburyUnderwater.Configuration;

public abstract class Settings
{
    /// <summary>
    ///     Binds the configuration section corresponding to the derived class name to its properties.
    ///     The "Settings" suffix in the class name is omitted when determining the configuration section.
    ///     e.g. "QuantumThreadingSettings" will be bound to a "QuantumThreading" configuration section.
    /// </summary>
    protected Settings(IConfiguration configuration)
    {
        var sectionName = GetType().Name;
        if (sectionName.EndsWith("Settings")) sectionName = sectionName[..^"Settings".Length];

        configuration.GetSection(sectionName).Bind(this);
    }
}