using Tiferet.Contexts;
using Tiferet.Mappers;

namespace Tiferet.Avalonia.Contexts;

// *** contexts

// ** context: domain_view_context
/// <summary>
/// A view context that wraps a Tiferet <see cref="Aggregate"/> and exposes it
/// as a bindable property. Provides a helper to execute features via
/// <see cref="AppInterfaceContext"/>.
/// </summary>
/// <typeparam name="TAggregate">The aggregate type to bind.</typeparam>
public abstract class DomainViewContext<TAggregate> : ViewContext
    where TAggregate : Aggregate
{
    // * attribute: app_context
    private readonly AppInterfaceContext? _appContext;

    // * attribute: state
    private TAggregate? _state;

    // *** properties

    // ** property: state
    /// <summary>
    /// The current aggregate state, bindable to the visual tree.
    /// </summary>
    public TAggregate? State
    {
        get => _state;
        protected set => SetProperty(ref _state, value);
    }

    // * init
    /// <summary>
    /// Initializes the domain view context with an optional <see cref="AppInterfaceContext"/>
    /// for feature execution.
    /// </summary>
    protected DomainViewContext(AppInterfaceContext? appContext = null)
    {
        _appContext = appContext;
    }

    // *** methods

    // ** method: execute_feature
    /// <summary>
    /// Execute a Tiferet feature by ID, passing the provided data dictionary.
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
}
