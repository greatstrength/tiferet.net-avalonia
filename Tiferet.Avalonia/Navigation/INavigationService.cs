using System.ComponentModel;

namespace Tiferet.Avalonia.Navigation;

// *** interfaces

// ** interface: navigation_service
/// <summary>
/// Page navigation service contract.
/// Typed on <see cref="INotifyPropertyChanged"/> so it works with both
/// <see cref="Contexts.ViewContext"/> and <see cref="Contexts.ViewModelBase"/> families.
/// </summary>
public interface INavigationService : INotifyPropertyChanged
{
    // * property: current_page
    /// <summary>
    /// The currently active page context (view model or view context).
    /// </summary>
    INotifyPropertyChanged? CurrentPage { get; }

    // * property: can_go_back
    /// <summary>
    /// Whether there is a previous page on the back stack.
    /// </summary>
    bool CanGoBack { get; }

    // * method: navigate_to
    /// <summary>
    /// Navigate to a page resolved by type.
    /// </summary>
    /// <typeparam name="TPage">The page context type to navigate to.</typeparam>
    /// <param name="parameter">Optional navigation parameter.</param>
    void NavigateTo<TPage>(object? parameter = null)
        where TPage : class, INotifyPropertyChanged;

    // * method: go_back
    /// <summary>
    /// Navigate to the previous page on the back stack.
    /// </summary>
    void GoBack();
}
