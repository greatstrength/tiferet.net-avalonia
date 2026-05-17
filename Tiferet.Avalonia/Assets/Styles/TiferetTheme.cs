using Avalonia.Markup.Xaml;
using Tiferet.Avalonia.Assets.Design;
using Tiferet.Avalonia.Blueprints;

namespace Tiferet.Avalonia.Assets.Styles;

// *** styles

// ** style: tiferet_theme
/// <summary>
/// The Tiferet design system theme. Drop-in entry point for Avalonia applications.
/// Loads color resources (Light/Dark), typography resources, and all default control themes.
/// Usage: <c>&lt;TiferetTheme /&gt;</c> in App.axaml or add programmatically via
/// <c>app.Styles.Add(new TiferetTheme())</c>.
/// </summary>
public class TiferetTheme : global::Avalonia.Styling.Styles
{
    // * init
    /// <summary>
    /// Initializes the Tiferet theme, loading all bundled resource dictionaries.
    /// </summary>
    public TiferetTheme()
    {
        AvaloniaXamlLoader.Load(this);
    }

    // * method: resolve_palette (static)
    /// <summary>
    /// Resolve the active <see cref="TiferetPalette"/> from the provided options.
    /// Priority: <see cref="TiferetAvaloniaOptions.CustomPalette"/> first,
    /// then <see cref="TiferetAvaloniaOptions.ThemeVariant"/>.
    /// </summary>
    /// <param name="options">The application options.</param>
    /// <returns>The resolved palette.</returns>
    public static TiferetPalette ResolvePalette(TiferetAvaloniaOptions options)
    {
        // Custom palette takes priority.
        if (options.CustomPalette is not null)
            return options.CustomPalette;

        // Resolve from theme variant string.
        return options.ThemeVariant?.ToLowerInvariant() switch
        {
            "dark" => TiferetPalette.Dark(),
            _ => TiferetPalette.Light(),
        };
    }
}
