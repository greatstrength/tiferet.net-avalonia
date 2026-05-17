using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Tiferet.Avalonia.Navigation;

namespace Tiferet.Avalonia.Tests.Navigation;

// *** fixtures

// ** fixture: stub pages

#pragma warning disable CS0067 // Event is never used (test stubs)

public class StubPage : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
}

public class StubPageB : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
}

public class NavigationAwarePage : INotifyPropertyChanged, INavigationAware
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public object? NavigatedToParameter { get; private set; }
    public bool WasNavigatedFrom { get; private set; }
    public int NavigatedToCount { get; private set; }
    public int NavigatedFromCount { get; private set; }

    public void OnNavigatedTo(object? parameter = null)
    {
        NavigatedToParameter = parameter;
        NavigatedToCount++;
    }

    public void OnNavigatedFrom()
    {
        WasNavigatedFrom = true;
        NavigatedFromCount++;
    }
}

#pragma warning restore CS0067

// *** tests

public class NavigationServiceTests
{
    private static (NavigationService svc, IServiceProvider sp) CreateService()
    {
        var services = new ServiceCollection();
        services.AddTransient<StubPage>();
        services.AddTransient<StubPageB>();
        services.AddTransient<NavigationAwarePage>();
        var sp = services.BuildServiceProvider();
        var factory = new PageFactory(sp);
        var nav = new NavigationService(factory);
        return (nav, sp);
    }

    // ** test: navigate_to_sets_current_page
    [Fact]
    public void NavigateTo_SetsCurrentPage()
    {
        var (nav, _) = CreateService();

        nav.NavigateTo<StubPage>();

        Assert.NotNull(nav.CurrentPage);
        Assert.IsType<StubPage>(nav.CurrentPage);
    }

    // ** test: navigate_to_pushes_back_stack
    [Fact]
    public void NavigateTo_PushesBackStack()
    {
        var (nav, _) = CreateService();

        nav.NavigateTo<StubPage>();
        Assert.False(nav.CanGoBack);

        nav.NavigateTo<StubPageB>();
        Assert.True(nav.CanGoBack);
        Assert.IsType<StubPageB>(nav.CurrentPage);
    }

    // ** test: go_back_restores_previous_page
    [Fact]
    public void GoBack_RestoresPreviousPage()
    {
        var (nav, _) = CreateService();

        nav.NavigateTo<StubPage>();
        nav.NavigateTo<StubPageB>();

        nav.GoBack();

        Assert.IsType<StubPage>(nav.CurrentPage);
        Assert.False(nav.CanGoBack);
    }

    // ** test: go_back_on_empty_stack_is_noop
    [Fact]
    public void GoBack_EmptyStack_IsNoOp()
    {
        var (nav, _) = CreateService();

        nav.GoBack(); // Should not throw.

        Assert.Null(nav.CurrentPage);
    }

    // ** test: navigate_to_notifies_navigation_aware
    [Fact]
    public void NavigateTo_NotifiesNavigationAware()
    {
        var (nav, _) = CreateService();

        nav.NavigateTo<NavigationAwarePage>(parameter: "hello");

        var page = nav.CurrentPage as NavigationAwarePage;
        Assert.NotNull(page);
        Assert.Equal("hello", page!.NavigatedToParameter);
        Assert.Equal(1, page.NavigatedToCount);
    }

    // ** test: navigate_away_notifies_source
    [Fact]
    public void NavigateAway_NotifiesSource()
    {
        var (nav, _) = CreateService();

        nav.NavigateTo<NavigationAwarePage>();
        var firstPage = nav.CurrentPage as NavigationAwarePage;

        nav.NavigateTo<StubPage>();

        Assert.NotNull(firstPage);
        Assert.True(firstPage!.WasNavigatedFrom);
        Assert.Equal(1, firstPage.NavigatedFromCount);
    }

    // ** test: go_back_notifies_both_pages
    [Fact]
    public void GoBack_NotifiesBothPages()
    {
        var (nav, _) = CreateService();

        nav.NavigateTo<NavigationAwarePage>();
        var firstPage = nav.CurrentPage as NavigationAwarePage;

        nav.NavigateTo<NavigationAwarePage>();
        var secondPage = nav.CurrentPage as NavigationAwarePage;

        nav.GoBack();

        // Second page was navigated from.
        Assert.True(secondPage!.WasNavigatedFrom);
        // First page was navigated to again.
        Assert.Equal(2, firstPage!.NavigatedToCount);
    }

    // ** test: property_changed_fires_for_current_page
    [Fact]
    public void PropertyChanged_FiresForCurrentPage()
    {
        var (nav, _) = CreateService();
        var changedProperties = new List<string>();
        nav.PropertyChanged += (_, e) => changedProperties.Add(e.PropertyName!);

        nav.NavigateTo<StubPage>();

        Assert.Contains("CurrentPage", changedProperties);
        Assert.Contains("CanGoBack", changedProperties);
    }
}
