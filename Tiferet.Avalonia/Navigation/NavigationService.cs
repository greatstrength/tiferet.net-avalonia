using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tiferet.Avalonia.Navigation;

// *** navigation

// ** navigation: navigation_service
/// <summary>
/// Concrete navigation service with back-stack management and
/// <see cref="INavigationAware"/> lifecycle notification.
/// </summary>
public class NavigationService : INavigationService
{
    // * attribute: page_factory
    private readonly PageFactory _pageFactory;

    // * attribute: back_stack
    private readonly Stack<INotifyPropertyChanged> _backStack = new();

    // * attribute: current_page
    private INotifyPropertyChanged? _currentPage;

    // * event: property_changed
    public event PropertyChangedEventHandler? PropertyChanged;

    // *** properties

    // ** property: current_page
    /// <summary>
    /// The currently active page context.
    /// </summary>
    public INotifyPropertyChanged? CurrentPage
    {
        get => _currentPage;
        private set
        {
            if (ReferenceEquals(_currentPage, value))
                return;

            _currentPage = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanGoBack));
        }
    }

    // ** property: can_go_back
    /// <summary>
    /// Whether there is a previous page on the back stack.
    /// </summary>
    public bool CanGoBack => _backStack.Count > 0;

    // * init
    /// <summary>
    /// Initializes the navigation service with a page factory.
    /// </summary>
    /// <param name="pageFactory">The DI-aware page factory for resolving page contexts.</param>
    public NavigationService(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;
    }

    // *** methods

    // ** method: navigate_to
    /// <summary>
    /// Navigate to a page resolved by type. Pushes the current page
    /// onto the back stack and notifies both source and target pages
    /// if they implement <see cref="INavigationAware"/>.
    /// </summary>
    /// <typeparam name="TPage">The page context type to navigate to.</typeparam>
    /// <param name="parameter">Optional navigation parameter.</param>
    public void NavigateTo<TPage>(object? parameter = null)
        where TPage : class, INotifyPropertyChanged
    {
        // Resolve the target page from DI.
        var target = _pageFactory.Create<TPage>();

        // Notify the current page it is being navigated away from.
        if (_currentPage is INavigationAware currentAware)
            currentAware.OnNavigatedFrom();

        // Push the current page onto the back stack.
        if (_currentPage is not null)
            _backStack.Push(_currentPage);

        // Set the new current page.
        CurrentPage = target;

        // Notify the target page it has been navigated to.
        if (target is INavigationAware targetAware)
            targetAware.OnNavigatedTo(parameter);
    }

    // ** method: go_back
    /// <summary>
    /// Navigate to the previous page on the back stack.
    /// Notifies both source and target pages if they implement <see cref="INavigationAware"/>.
    /// </summary>
    public void GoBack()
    {
        if (_backStack.Count == 0)
            return;

        // Notify the current page it is being navigated away from.
        if (_currentPage is INavigationAware currentAware)
            currentAware.OnNavigatedFrom();

        // Pop the previous page from the back stack.
        var previous = _backStack.Pop();

        // Set the previous page as current.
        CurrentPage = previous;

        // Notify the previous page it has been navigated to.
        if (previous is INavigationAware previousAware)
            previousAware.OnNavigatedTo();
    }

    // * method: on_property_changed
    /// <summary>
    /// Raise <see cref="PropertyChanged"/> for the specified property.
    /// </summary>
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
