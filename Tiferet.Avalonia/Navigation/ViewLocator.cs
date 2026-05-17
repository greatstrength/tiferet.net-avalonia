using System.ComponentModel;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Tiferet.Avalonia.Navigation;

// *** navigation

// ** navigation: view_locator
/// <summary>
/// <see cref="IDataTemplate"/> implementation that maps context types to view types
/// by naming convention. Resolves <c>*ViewModel</c> → <c>*View</c> and
/// <c>*ViewContext</c> → <c>*View</c> within the same assembly.
/// Falls back to a <see cref="TextBlock"/> for unresolved types.
/// </summary>
public class ViewLocator : IDataTemplate
{
    // * attribute: view_assemblies
    private readonly Assembly[] _viewAssemblies;

    // * init
    /// <summary>
    /// Initializes the view locator.
    /// </summary>
    /// <param name="viewAssemblies">
    /// Assemblies to scan for view types. If empty, the entry assembly is used.
    /// </param>
    public ViewLocator(params Assembly[] viewAssemblies)
    {
        _viewAssemblies = viewAssemblies.Length > 0
            ? viewAssemblies
            : new[] { Assembly.GetEntryAssembly()! };
    }

    // * method: match
    /// <summary>
    /// Returns true when the data object implements <see cref="INotifyPropertyChanged"/>,
    /// indicating this locator can resolve a view for it.
    /// </summary>
    public bool Match(object? data) => data is INotifyPropertyChanged;

    // * method: build
    /// <summary>
    /// Build the view control for the given data context.
    /// Resolves the view type by naming convention and falls back to a
    /// <see cref="TextBlock"/> displaying the type name if no view is found.
    /// </summary>
    public Control Build(object? data)
    {
        if (data is null)
            return new TextBlock { Text = "null" };

        // Resolve the view type name from the context type name.
        var contextTypeName = data.GetType().FullName!;
        var viewTypeName = ResolveViewTypeName(contextTypeName);

        // Search for the view type across registered assemblies.
        var viewType = ResolveViewType(viewTypeName);

        // If found, instantiate the view.
        if (viewType is not null)
            return (Control)Activator.CreateInstance(viewType)!;

        // Fallback: display the unresolved type name.
        return new TextBlock { Text = $"View not found: {viewTypeName}" };
    }

    // * method: resolve_view_type_name
    /// <summary>
    /// Convert a context type name to a view type name by convention.
    /// <c>*ViewModel</c> → <c>*View</c>, <c>*ViewContext</c> → <c>*View</c>.
    /// </summary>
    private static string ResolveViewTypeName(string contextTypeName)
    {
        // Try ViewModel suffix first.
        if (contextTypeName.EndsWith("ViewModel"))
            return contextTypeName[..^"Model".Length];

        // Try ViewContext suffix.
        if (contextTypeName.EndsWith("ViewContext"))
            return contextTypeName[..^"Context".Length];

        // Fallback: append "View".
        return contextTypeName + "View";
    }

    // * method: resolve_view_type
    /// <summary>
    /// Search registered assemblies for a type matching the given name.
    /// </summary>
    private Type? ResolveViewType(string viewTypeName)
    {
        foreach (var assembly in _viewAssemblies)
        {
            var viewType = assembly.GetType(viewTypeName);
            if (viewType is not null)
                return viewType;
        }

        return null;
    }
}
