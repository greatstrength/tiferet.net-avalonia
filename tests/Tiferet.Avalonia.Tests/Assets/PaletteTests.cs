using Avalonia.Media;
using Tiferet.Avalonia.Assets.Design;
using Tiferet.Avalonia.Assets.Styles;
using Tiferet.Avalonia.Blueprints;

namespace Tiferet.Avalonia.Tests.Assets;

// *** tests

public class PaletteTests
{
    // ** test: light_returns_default_palette
    [Fact]
    public void Light_ReturnsDefaultPalette()
    {
        var palette = TiferetPalette.Light();

        Assert.NotNull(palette.Primary);
        Assert.NotNull(palette.Secondary);
        Assert.NotNull(palette.Neutral);
        Assert.NotNull(palette.Semantic);
    }

    // ** test: dark_returns_dark_palette
    [Fact]
    public void Dark_ReturnsDarkPalette()
    {
        var palette = TiferetPalette.Dark();

        Assert.NotNull(palette.Primary);
        // Dark background should differ from light.
        Assert.NotEqual(TiferetPalette.Light().Background, palette.Background);
    }

    // ** test: custom_with_no_overrides_equals_base
    [Fact]
    public void Custom_NoOverrides_EqualsBase()
    {
        var light = TiferetPalette.Light();
        var custom = TiferetPalette.Custom(basePalette: light);

        Assert.Equal(light, custom);
    }

    // ** test: custom_overrides_primary
    [Fact]
    public void Custom_OverridesPrimary()
    {
        var light = TiferetPalette.Light();
        var redScale = new ColorScale(
            S50:  Color.Parse("#FFF5F5"),
            S100: Color.Parse("#FED7D7"),
            S200: Color.Parse("#FEB2B2"),
            S300: Color.Parse("#FC8181"),
            S400: Color.Parse("#F56565"),
            S500: Color.Parse("#E53E3E"),
            S600: Color.Parse("#C53030"),
            S700: Color.Parse("#9B2C2C"),
            S800: Color.Parse("#822727"),
            S900: Color.Parse("#63171B"));

        var custom = TiferetPalette.Custom(basePalette: light, primary: redScale);

        Assert.Equal(redScale, custom.Primary);
        // Non-overridden fields inherit from base.
        Assert.Equal(light.Secondary, custom.Secondary);
        Assert.Equal(light.Background, custom.Background);
    }

    // ** test: custom_overrides_background
    [Fact]
    public void Custom_OverridesBackground()
    {
        var dark = TiferetPalette.Dark();
        var customBg = Color.Parse("#1A1A2E");

        var custom = TiferetPalette.Custom(basePalette: dark, background: customBg);

        Assert.Equal(customBg, custom.Background);
        Assert.Equal(dark.Primary, custom.Primary);
    }

    // ** test: custom_defaults_to_light_when_no_base
    [Fact]
    public void Custom_DefaultsToLight_WhenNoBase()
    {
        var light = TiferetPalette.Light();
        var custom = TiferetPalette.Custom();

        Assert.Equal(light, custom);
    }

    // ** test: resolve_palette_returns_custom_when_set
    [Fact]
    public void ResolvePalette_ReturnsCustom_WhenSet()
    {
        var custom = TiferetPalette.Dark();
        var options = new TiferetAvaloniaOptions { CustomPalette = custom };

        var resolved = TiferetTheme.ResolvePalette(options);

        Assert.Same(custom, resolved);
    }

    // ** test: resolve_palette_returns_dark_for_dark_variant
    [Fact]
    public void ResolvePalette_ReturnsDark_ForDarkVariant()
    {
        var options = new TiferetAvaloniaOptions { ThemeVariant = "Dark" };

        var resolved = TiferetTheme.ResolvePalette(options);

        Assert.Equal(TiferetPalette.Dark(), resolved);
    }

    // ** test: resolve_palette_returns_light_for_default_variant
    [Fact]
    public void ResolvePalette_ReturnsLight_ForDefaultVariant()
    {
        var options = new TiferetAvaloniaOptions { ThemeVariant = "Default" };

        var resolved = TiferetTheme.ResolvePalette(options);

        Assert.Equal(TiferetPalette.Light(), resolved);
    }
}
