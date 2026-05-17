using Microsoft.Extensions.DependencyInjection;
using Tiferet.Avalonia.Blueprints;
using Tiferet.Avalonia.Navigation;

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

    // ** method: add_tiferet_navigation
    /// <summary>
    /// Add Tiferet navigation services (<see cref="PageFactory"/>,
    /// <see cref="NavigationService"/>, and <see cref="INavigationService"/>)
    /// to the DI container.
    /// </summary>
    public static IServiceCollection AddTiferetNavigation(
        this IServiceCollection services)
    {
        services.AddSingleton<PageFactory>();
        services.AddSingleton<NavigationService>();
        services.AddSingleton<INavigationService>(sp => sp.GetRequiredService<NavigationService>());
        return services;
    }
}
