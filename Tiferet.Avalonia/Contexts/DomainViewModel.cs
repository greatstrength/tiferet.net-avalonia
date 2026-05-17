using CommunityToolkit.Mvvm.ComponentModel;
using Tiferet.Contexts;
using Tiferet.Domain;

namespace Tiferet.Avalonia.Contexts;

// *** contexts

// ** context: domain_view_model
/// <summary>
/// MVVM counterpart to <see cref="DomainViewContext{T}"/>.
/// Wraps a <see cref="DomainObject"/> as bindable state and provides
/// helpers to execute features via <see cref="AppInterfaceContext"/>.
/// Constrains on <see cref="DomainObject"/> (not Aggregate) to maintain
/// separation between the view layer and the mapper layer.
/// </summary>
/// <typeparam name="TModel">The domain model type to bind.</typeparam>
public abstract partial class DomainViewModel<TModel> : ViewModelBase
    where TModel : DomainObject
{
    // * attribute: state
    [ObservableProperty]
    private TModel? _state;

    // * attribute: app_context
    private readonly AppInterfaceContext? _appContext;

    // * init
    /// <summary>
    /// Initializes the domain view model with an optional <see cref="AppInterfaceContext"/>
    /// for feature execution.
    /// </summary>
    /// <param name="appContext">The application interface context. May be null for design-time or testing.</param>
    protected DomainViewModel(AppInterfaceContext? appContext = null)
    {
        _appContext = appContext;
    }

    // *** methods

    // ** method: execute_feature
    /// <summary>
    /// Execute a Tiferet feature synchronously by ID, passing the provided data dictionary.
    /// Returns the feature result.
    /// </summary>
    /// <param name="featureId">The feature identifier (e.g., "calc.add").</param>
    /// <param name="data">The input data dictionary.</param>
    /// <returns>The feature execution result.</returns>
    protected object? ExecuteFeature(string featureId, Dictionary<string, object?> data)
    {
        if (_appContext is null)
            throw new InvalidOperationException(
                "AppInterfaceContext is not available. Provide it via the constructor.");

        return _appContext.Run(featureId, data: data);
    }

    // ** method: execute_feature_async
    /// <summary>
    /// Execute a Tiferet feature asynchronously by ID, passing the provided data dictionary.
    /// Supports <see cref="Tiferet.Events.AsyncDomainEvent"/> steps without deadlocking.
    /// </summary>
    /// <param name="featureId">The feature identifier (e.g., "calc.add").</param>
    /// <param name="data">The input data dictionary.</param>
    /// <returns>The feature execution result.</returns>
    protected Task<object?> ExecuteFeatureAsync(string featureId, Dictionary<string, object?> data)
    {
        if (_appContext is null)
            throw new InvalidOperationException(
                "AppInterfaceContext is not available. Provide it via the constructor.");

        return _appContext.RunAsync(featureId, data: data);
    }
}
