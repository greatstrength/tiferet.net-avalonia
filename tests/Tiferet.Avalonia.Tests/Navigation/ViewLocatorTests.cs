using System.ComponentModel;
using System.Reflection;
using Avalonia.Controls;
using Tiferet.Avalonia.Navigation;

namespace Tiferet.Avalonia.Tests.Navigation;

// *** fixtures

// ** fixture: stub view model and view for naming convention tests

#pragma warning disable CS0067 // Event is never used (test stubs)

public class SampleViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
}

public class SampleView : UserControl { }

public class SampleViewContext : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
}

public class OrphanViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
}

#pragma warning restore CS0067

// *** tests

public class ViewLocatorTests
{
    private ViewLocator CreateLocator() =>
        new ViewLocator(Assembly.GetExecutingAssembly());

    // ** test: match_returns_true_for_inpc
    [Fact]
    public void Match_ReturnsTrue_ForINPC()
    {
        var locator = CreateLocator();

        Assert.True(locator.Match(new SampleViewModel()));
    }

    // ** test: match_returns_false_for_non_inpc
    [Fact]
    public void Match_ReturnsFalse_ForNonINPC()
    {
        var locator = CreateLocator();

        Assert.False(locator.Match("not a view model"));
        Assert.False(locator.Match(null));
    }

    // ** test: build_resolves_view_model_to_view
    [Fact]
    public void Build_ResolvesViewModel_ToView()
    {
        var locator = CreateLocator();
        var vm = new SampleViewModel();

        var control = locator.Build(vm);

        Assert.IsType<SampleView>(control);
    }

    // ** test: build_returns_fallback_for_unresolved
    [Fact]
    public void Build_ReturnsFallback_ForUnresolved()
    {
        var locator = CreateLocator();
        var vm = new OrphanViewModel();

        var control = locator.Build(vm);

        Assert.IsType<TextBlock>(control);
        var text = (TextBlock)control;
        Assert.Contains("View not found", text.Text);
    }

    // ** test: build_returns_textblock_for_null
    [Fact]
    public void Build_ReturnsTextBlock_ForNull()
    {
        var locator = CreateLocator();

        var control = locator.Build(null);

        Assert.IsType<TextBlock>(control);
        Assert.Equal("null", ((TextBlock)control).Text);
    }
}
