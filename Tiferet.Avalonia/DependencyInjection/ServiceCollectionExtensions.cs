using Microsoft.Extensions.DependencyInjection;
using Tiferet.Avalonia.Blueprints;

namespace Tiferet.Avalonia.DependencyInjection;

// *** extensions

// ** extension: service_collection_extensions
/// <summary>
/// Extension methods for integrating Tiferet.Avalonia with
/// <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    // ** method: add_tiferet_avalonia
    /// <summary>
    /// Add Tiferet framework services and Avalonia design system services
    /// to the DI container, using an explicit configuration callback.
    /// </summary>
    public static IServiceCollection AddTiferetAvalonia(
        this IServiceCollection services,
        Action<TiferetAvaloniaOptions> configure)
    {
        var options = new TiferetAvaloniaOptions();
        configure(options);
        return AvaloniaBlueprint.ConfigureServices(services, options);
    }
}
