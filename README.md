# Tiferet.Avalonia

Design system, controls, navigation, and view contexts for building cross-platform Avalonia UI applications with the [Tiferet.NET](https://github.com/greatstrength/tiferet.net) DDD framework.

## Features

- **Design System** — Palette, typography scale, spacing/corner-radius/elevation tokens, and themed controls (`TiferetBadge`, `TiferetButton`, `TiferetCard`, `TiferetTextBlock`)
- **View Contexts** — `ViewContext` (manual INPC) and `DomainViewContext<TModel>` for binding domain models with Tiferet feature execution
- **MVVM Base Classes** — `ViewModelBase`, `PageViewModel`, and `DomainViewModel<TModel>` powered by [CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/) source generators
- **Navigation Infrastructure** — `INavigationService`, `NavigationService` (back-stack), `PageFactory` (DI-aware), `ViewLocator` (convention-based `IDataTemplate`)
- **Custom Palette Support** — `TiferetPalette.Custom(...)` for domain-specific color schemes (military, medical, industrial, etc.)
- **Async Feature Execution** — `ExecuteFeatureAsync()` on both view families, backed by Tiferet.NET `AsyncDomainEvent` and `AppInterfaceContext.RunAsync()`
- **DI Integration** — `AddTiferetAvalonia()` and `AddTiferetNavigation()` extension methods for `IServiceCollection`

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Tiferet.NET ≥ 1.0.0-beta.7

### Installation

Add the `Tiferet.Avalonia` project reference or NuGet package to your Avalonia application.

### Quick Start

```csharp
// Program.cs — DI-based bootstrap
services.AddTiferetAvalonia(options =>
{
    options.InterfaceId = "my_app";
    options.ConfigDir = "app/configs";
    options.ThemeVariant = "Dark";
});
services.AddTiferetNavigation();
```

### Choosing a Base Class

| Need | Base Class | Key Benefit |
|------|-----------|-------------|
| Minimal, no dependencies | `ViewContext` | Manual `SetProperty<T>` INPC |
| Source-generated properties/commands | `ViewModelBase` | `[ObservableProperty]`, `[RelayCommand]` |
| Navigable page with design-time support | `PageViewModel` | `OnDesignTimeConstructor()` |
| Domain model binding (manual INPC) | `DomainViewContext<TModel>` | `State` + `ExecuteFeature()` |
| Domain model binding (toolkit) | `DomainViewModel<TModel>` | `[ObservableProperty] State` + `ExecuteFeatureAsync()` |

### Custom Palette

```csharp
var militaryPalette = TiferetPalette.Custom(
    basePalette: TiferetPalette.Dark(),
    primary: new ColorScale(/* olive drab scale */),
    background: Color.Parse("#1A1A1A"));

services.AddTiferetAvalonia(options =>
{
    options.CustomPalette = militaryPalette;
});
```

## Project Structure

```
Tiferet.Avalonia/
├── Assets/              Design system (palette, typography, tokens, controls, styles)
├── Blueprints/          Bootstrap wiring (AvaloniaBlueprint, TiferetAvaloniaOptions)
├── Contexts/            View base classes (ViewContext, ViewModelBase, DomainViewModel, etc.)
├── DependencyInjection/ IServiceCollection extensions
├── Domain/              Domain records (ThemeConfiguration, DesignTokenConfiguration)
└── Navigation/          Navigation infrastructure (INavigationService, ViewLocator, etc.)

tests/Tiferet.Avalonia.Tests/
├── Assets/              Palette and theme tests
├── Contexts/            DomainViewModel and DomainViewContext tests
└── Navigation/          NavigationService, PageFactory, ViewLocator tests

examples/Tiferet.Avalonia.Examples.Calculator/
└── ...                  Calculator demo app
```

## Dependencies

- [Avalonia](https://avaloniaui.net/) 11.2.7
- [Tiferet.NET](https://github.com/greatstrength/tiferet.net) ≥ 1.0.0-beta.7
- [CommunityToolkit.Mvvm](https://www.nuget.org/packages/CommunityToolkit.Mvvm) 8.4.0

## License

MIT — see [LICENSE](LICENSE).
