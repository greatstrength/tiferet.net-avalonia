using Avalonia.Markup.Xaml;

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
}
