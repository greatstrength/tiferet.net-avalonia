using Avalonia.Controls;

namespace Tiferet.Avalonia.Contexts;

// *** contexts

// ** context: page_view_model
/// <summary>
/// Base class for navigable page view models.
/// Extends <see cref="ViewModelBase"/> with design-time detection support,
/// allowing subclasses to populate sample data for the Avalonia designer.
/// </summary>
public partial class PageViewModel : ViewModelBase
{
    // * init
    /// <summary>
    /// Initializes the page view model.
    /// Invokes <see cref="OnDesignTimeConstructor"/> when running in the Avalonia designer.
    /// </summary>
    public PageViewModel()
    {
        if (Design.IsDesignMode)
            OnDesignTimeConstructor();
    }

    // * method: on_design_time_constructor
    /// <summary>
    /// Override to populate sample data when the Avalonia designer is active.
    /// Default implementation does nothing.
    /// </summary>
    protected virtual void OnDesignTimeConstructor() { }
}
