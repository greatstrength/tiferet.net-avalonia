using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Tiferet.Avalonia.Navigation;

// *** navigation

// ** navigation: page_factory
/// <summary>
/// DI-aware page factory that resolves page contexts (view models or view contexts)
/// from the service provider.
/// </summary>
public class PageFactory
{
    // * attribute: service_provider
    private readonly IServiceProvider _serviceProvider;

    // * init
    /// <summary>
    /// Initializes the page factory with a service provider.
    /// </summary>
    /// <param name="serviceProvider">The DI service provider.</param>
    public PageFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    // * method: create
    /// <summary>
    /// Resolve a page context by type from the DI container.
    /// </summary>
    /// <typeparam name="TPage">The page context type.</typeparam>
    /// <returns>The resolved page context instance.</returns>
    public TPage Create<TPage>()
        where TPage : class, INotifyPropertyChanged
    {
        return _serviceProvider.GetRequiredService<TPage>();
    }

    // * method: create_by_type
    /// <summary>
    /// Resolve a page context by runtime type from the DI container.
    /// </summary>
    /// <param name="pageType">The page context type.</param>
    /// <returns>The resolved page context instance.</returns>
    public INotifyPropertyChanged Create(Type pageType)
    {
        var service = _serviceProvider.GetRequiredService(pageType);

        if (service is not INotifyPropertyChanged page)
            throw new InvalidOperationException(
                $"Type {pageType.FullName} does not implement INotifyPropertyChanged.");

        return page;
    }
}
