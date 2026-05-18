using Avalonia.Media;
using Tiferet.Avalonia.Assets.Controls;
using Tiferet.Avalonia.Assets.Design;

namespace Tiferet.Avalonia.Tests.Assets;

// *** tests

// ** test: form_controls
public class FormControlTests
{
    // ** test: text_box_defaults
    [Fact]
    public void TiferetTextBox_HasCorrectDefaults()
    {
        var control = new TiferetTextBox();

        Assert.Null(control.Text);
        Assert.Null(control.Placeholder);
        Assert.False(control.IsReadOnly);
        Assert.Equal("outlined", control.Variant);
    }

    // ** test: text_box_set_properties
    [Fact]
    public void TiferetTextBox_SetProperties()
    {
        var control = new TiferetTextBox
        {
            Text = "Hello",
            Placeholder = "Enter text",
            IsReadOnly = true,
            Variant = "filled"
        };

        Assert.Equal("Hello", control.Text);
        Assert.Equal("Enter text", control.Placeholder);
        Assert.True(control.IsReadOnly);
        Assert.Equal("filled", control.Variant);
    }

    // ** test: combo_box_defaults
    [Fact]
    public void TiferetComboBox_HasCorrectDefaults()
    {
        var control = new TiferetComboBox();

        Assert.Null(control.Items);
        Assert.Null(control.SelectedItem);
        Assert.Null(control.Placeholder);
        Assert.Equal("outlined", control.Variant);
    }

    // ** test: combo_box_set_properties
    [Fact]
    public void TiferetComboBox_SetProperties()
    {
        var items = new[] { "A", "B", "C" };
        var control = new TiferetComboBox
        {
            Items = items,
            SelectedItem = "B",
            Placeholder = "Pick one",
            Variant = "filled"
        };

        Assert.Same(items, control.Items);
        Assert.Equal("B", control.SelectedItem);
        Assert.Equal("Pick one", control.Placeholder);
        Assert.Equal("filled", control.Variant);
    }

    // ** test: toggle_switch_defaults
    [Fact]
    public void TiferetToggleSwitch_HasCorrectDefaults()
    {
        var control = new TiferetToggleSwitch();

        Assert.False(control.IsChecked);
        Assert.Null(control.OnLabel);
        Assert.Null(control.OffLabel);
        Assert.Equal("md", control.Size);
    }

    // ** test: toggle_switch_set_properties
    [Fact]
    public void TiferetToggleSwitch_SetProperties()
    {
        var control = new TiferetToggleSwitch
        {
            IsChecked = true,
            OnLabel = "On",
            OffLabel = "Off",
            Size = "sm"
        };

        Assert.True(control.IsChecked);
        Assert.Equal("On", control.OnLabel);
        Assert.Equal("Off", control.OffLabel);
        Assert.Equal("sm", control.Size);
    }

    // ** test: slider_defaults
    [Fact]
    public void TiferetSlider_HasCorrectDefaults()
    {
        var control = new TiferetSlider();

        Assert.Equal(0, control.Value);
        Assert.Equal(0, control.Minimum);
        Assert.Equal(100, control.Maximum);
        Assert.Equal(1, control.Step);
        Assert.False(control.ShowLabel);
    }

    // ** test: slider_set_properties
    [Fact]
    public void TiferetSlider_SetProperties()
    {
        var control = new TiferetSlider
        {
            Value = 50,
            Minimum = 10,
            Maximum = 200,
            Step = 5,
            ShowLabel = true
        };

        Assert.Equal(50, control.Value);
        Assert.Equal(10, control.Minimum);
        Assert.Equal(200, control.Maximum);
        Assert.Equal(5, control.Step);
        Assert.True(control.ShowLabel);
    }
}

// ** test: loading_controls
public class LoadingControlTests
{
    // ** test: spinner_defaults
    [Fact]
    public void TiferetSpinner_HasCorrectDefaults()
    {
        var control = new TiferetSpinner();

        Assert.Equal("md", control.Size);
        Assert.True(control.IsActive);
    }

    // ** test: spinner_set_properties
    [Fact]
    public void TiferetSpinner_SetProperties()
    {
        var control = new TiferetSpinner
        {
            Size = "lg",
            IsActive = false
        };

        Assert.Equal("lg", control.Size);
        Assert.False(control.IsActive);
    }

    // ** test: skeleton_defaults
    [Fact]
    public void TiferetSkeleton_HasCorrectDefaults()
    {
        var control = new TiferetSkeleton();

        Assert.Equal("rectangular", control.Variant);
    }

    // ** test: skeleton_set_variant
    [Fact]
    public void TiferetSkeleton_SetVariant()
    {
        var control = new TiferetSkeleton { Variant = "circular" };

        Assert.Equal("circular", control.Variant);
    }
}

// ** test: icon_system
public class IconTests
{
    // ** test: icon_defaults
    [Fact]
    public void TiferetIcon_HasCorrectDefaults()
    {
        var control = new TiferetIcon();

        Assert.Null(control.Kind);
        Assert.Equal(24, control.Size);
        Assert.Null(control.Color);
        Assert.Null(control.Geometry);
    }

    // ** test: icon_set_properties
    [Fact]
    public void TiferetIcon_SetProperties()
    {
        var brush = new SolidColorBrush(Colors.Red);
        var control = new TiferetIcon
        {
            Kind = "home",
            Size = 32,
            Color = brush
        };

        Assert.Equal("home", control.Kind);
        Assert.Equal(32, control.Size);
        Assert.Same(brush, control.Color);
    }

    // ** test: icons_registry_known_icon
    [Fact]
    public void TiferetIcons_Get_ReturnsPathForKnownIcon()
    {
        var path = TiferetIcons.Get("home");

        Assert.NotNull(path);
        Assert.NotEmpty(path!);
    }

    // ** test: icons_registry_unknown_icon
    [Fact]
    public void TiferetIcons_Get_ReturnsNullForUnknownIcon()
    {
        var path = TiferetIcons.Get("nonexistent_icon_xyz");

        Assert.Null(path);
    }

    // ** test: icons_registry_case_insensitive
    [Fact]
    public void TiferetIcons_Get_IsCaseInsensitive()
    {
        var lower = TiferetIcons.Get("home");
        var upper = TiferetIcons.Get("HOME");
        var mixed = TiferetIcons.Get("Home");

        Assert.Equal(lower, upper);
        Assert.Equal(lower, mixed);
    }

    // ** test: icons_registry_exists
    [Fact]
    public void TiferetIcons_Exists_ReturnsTrueForKnown()
    {
        Assert.True(TiferetIcons.Exists("search"));
        Assert.False(TiferetIcons.Exists("nonexistent"));
    }

    // ** test: icons_registry_all_names
    [Fact]
    public void TiferetIcons_AllNames_ContainsExpectedIcons()
    {
        var names = TiferetIcons.AllNames;

        Assert.Contains("home", names);
        Assert.Contains("search", names);
        Assert.Contains("close", names);
        Assert.Contains("warning", names);
        Assert.True(names.Count >= 20);
    }

    // ** test: icons_all_have_path_data
    [Fact]
    public void TiferetIcons_AllEntries_HaveNonEmptyPathData()
    {
        foreach (var name in TiferetIcons.AllNames)
        {
            var pathData = TiferetIcons.Get(name);
            Assert.NotNull(pathData);
            Assert.NotEmpty(pathData!);

            // Verify path data starts with a valid SVG command character.
            Assert.True("MmLlHhVvCcSsQqTtAaZz".Contains(pathData![0]),
                $"Icon '{name}' path data starts with unexpected char '{pathData[0]}'");
        }
    }
}
