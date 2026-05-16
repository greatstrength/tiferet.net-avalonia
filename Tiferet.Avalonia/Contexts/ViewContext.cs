using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tiferet.Avalonia.Contexts;

// *** contexts

// ** context: view_context
/// <summary>
/// Base view context providing <see cref="INotifyPropertyChanged"/> support.
/// Serves the role of a ViewModel in traditional MVVM, renamed to Context
/// to align with Tiferet's architectural vocabulary.
/// </summary>
public abstract class ViewContext : INotifyPropertyChanged
{
    // * event: property_changed
    public event PropertyChangedEventHandler? PropertyChanged;

    // * method: set_property
    /// <summary>
    /// Set a backing field and raise <see cref="PropertyChanged"/> if the value changed.
    /// </summary>
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    // * method: on_property_changed
    /// <summary>
    /// Raise <see cref="PropertyChanged"/> for the specified property.
    /// </summary>
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
