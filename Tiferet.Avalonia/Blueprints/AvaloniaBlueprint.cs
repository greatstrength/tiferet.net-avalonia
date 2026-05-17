using Microsoft.Extensions.DependencyInjection;
using Tiferet.Avalonia.Assets.Design;
using Tiferet.Avalonia.Assets.Styles;
using Tiferet.Avalonia.Contexts;
using Tiferet.Blueprints;
using Tiferet.Contexts;

namespace Tiferet.Avalonia.Blueprints;

// *** blueprints

// ** blueprint: avalonia_blueprint
/// <summary>
/// Static blueprint for bootstrapping Tiferet Avalonia applications.
/// Delegates core framework wiring to <see cref="AppBlueprint"/> and layers
/// Avalonia-specific services (theme, view contexts) on top.
/// </summary>
public static class AvaloniaBlueprint
{
    // ** method: build_app
    /// <summary>
    /// Build a fully wired <see cref="AppInterfaceContext"/> from the given options.
    /// Standalone mode — no external DI container required.
    /// </summary>
    public static AppInterfaceContext BuildApp(TiferetAvaloniaOptions options)
    {
        var configDir = options.ConfigDir;
        return AppBlueprint.BuildApp(options.InterfaceId, configDir);
    }

    // ** method: configure_services
    /// <summary>
    /// Register all Tiferet framework services plus Avalonia-specific services
    /// into an <see cref="IServiceCollection"/>.
    /// </summary>
    public static IServiceCollection ConfigureServices(
        IServiceCollection services,
        TiferetAvaloniaOptions options)
    {
        // Wire core Tiferet services via the framework blueprint.
        var coreOptions = new TiferetOptions
        {
            InterfaceId = options.InterfaceId,
            ConfigDir = options.ConfigDir,
            ConfigFile = options.ConfigFile,
        };
        AppBlueprint.ConfigureServices(services, coreOptions);

        // Register the TiferetTheme as a singleton.
        services.AddSingleton<TiferetTheme>();

        // Resolve and register the active palette from options.
        var palette = TiferetTheme.ResolvePalette(options);
        services.AddSingleton(palette);

        // Register the options for injection.
        services.AddSingleton(options);

        return services;
    }
}
