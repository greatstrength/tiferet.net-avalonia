using CommunityToolkit.Mvvm.ComponentModel;

namespace Tiferet.Avalonia.Contexts;

// *** contexts

// ** context: view_model_base
/// <summary>
/// MVVM base class extending <see cref="ObservableObject"/> from CommunityToolkit.Mvvm.
/// Serves as the toolkit-powered counterpart to <see cref="ViewContext"/>.
/// Consumers extend this and use <c>[ObservableProperty]</c>, <c>[RelayCommand]</c>,
/// <c>[NotifyPropertyChangedFor]</c> from CommunityToolkit.Mvvm directly.
/// </summary>
public abstract partial class ViewModelBase : ObservableObject
{
}
