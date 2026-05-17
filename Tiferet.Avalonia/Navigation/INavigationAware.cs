namespace Tiferet.Avalonia.Navigation;

// *** interfaces

// ** interface: navigation_aware
/// <summary>
/// Lifecycle hooks for view models and view contexts that need to respond
/// to navigation events. Works with both <see cref="Contexts.ViewContext"/>
/// and <see cref="Contexts.ViewModelBase"/> families.
/// </summary>
public interface INavigationAware
{
    // * method: on_navigated_to
    /// <summary>
    /// Called when the page is navigated to.
    /// </summary>
    /// <param name="parameter">Optional navigation parameter passed by the caller.</param>
    void OnNavigatedTo(object? parameter = null);

    // * method: on_navigated_from
    /// <summary>
    /// Called when the page is navigated away from.
    /// </summary>
    void OnNavigatedFrom();
}
