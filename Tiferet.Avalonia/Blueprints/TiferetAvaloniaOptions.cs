using Tiferet.Avalonia.Assets;

namespace Tiferet.Avalonia.Blueprints;

// *** blueprints

// ** blueprint: tiferet_avalonia_options
/// <summary>
/// Configuration POCO for bootstrapping a Tiferet Avalonia application.
/// Extends the core <see cref="Tiferet.Blueprints.TiferetOptions"/> concept
/// with UI-specific settings.
/// </summary>
public class TiferetAvaloniaOptions
{
    // ** property: interface_id
    /// <summary>The Tiferet application interface identifier.</summary>
    public string InterfaceId { get; set; } = "default";

    // ** property: config_dir
    /// <summary>The directory containing Tiferet configuration files.</summary>
    public string ConfigDir { get; set; } = Tiferet.Assets.ConfigurationDefaults.DefaultConfigDir;

    // ** property: config_file
    /// <summary>The consolidated configuration file name.</summary>
    public string ConfigFile { get; set; } = Tiferet.Assets.ConfigurationDefaults.ConfigFile;

    // ** property: theme_variant
    /// <summary>Theme variant: "Light", "Dark", or "Default" (system).</summary>
    public string ThemeVariant { get; set; } = DesignDefaults.DefaultThemeVariant;

    // ** property: primary_color
    /// <summary>Optional hex override for the primary palette color.</summary>
    public string? PrimaryColor { get; set; }

    // ** property: font_family
    /// <summary>Optional font family override.</summary>
    public string? FontFamily { get; set; }
}
