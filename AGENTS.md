# AGENTS.md — Tiferet.Avalonia (v1.0.0-beta.2)

## Project Overview

**Tiferet.Avalonia** is an Avalonia UI framework layer built on top of [Tiferet.NET](https://github.com/greatstrength/tiferet.net). It provides a design system, themed controls, view base classes, page navigation, and DI integration for building cross-platform desktop applications using Domain-Driven Design.

- **Repository:** https://github.com/greatstrength/tiferet.net-avalonia
- **Branch:** `v1.x-proto`
- **Runtime:** .NET 9.0
- **Version:** 1.0.0-beta.2
- **Core dependency:** Tiferet.NET ≥ 1.0.0-beta.7

## Architecture

### Layer Overview

```
Tiferet.Avalonia/
├── Assets/              Design system — palette, typography, tokens, controls, AXAML styles
├── Blueprints/          Bootstrap — AvaloniaBlueprint, TiferetAvaloniaOptions
├── Contexts/            View base classes — ViewContext, ViewModelBase, PageViewModel,
│                        DomainViewContext<T>, DomainViewModel<T>
├── DependencyInjection/ IServiceCollection extensions (AddTiferetAvalonia, AddTiferetNavigation)
├── Domain/              Domain records (ThemeConfiguration, DesignTokenConfiguration)
└── Navigation/          INavigationService, NavigationService, PageFactory, ViewLocator,
                         INavigationAware
```

### Key Concepts

- **ViewContext** — Manual `INotifyPropertyChanged` base class (lightweight, no external deps).
- **ViewModelBase** — CommunityToolkit.Mvvm `ObservableObject` base class. Enables `[ObservableProperty]`, `[RelayCommand]`, `[NotifyPropertyChangedFor]`.
- **PageViewModel** — Extends `ViewModelBase` with Avalonia design-time detection (`Design.IsDesignMode` → `OnDesignTimeConstructor()`).
- **DomainViewContext<TModel>** — Wraps a `DomainObject` as bindable `State`, provides `ExecuteFeature()` and `ExecuteFeatureAsync()` via `AppInterfaceContext`.
- **DomainViewModel<TModel>** — MVVM counterpart to `DomainViewContext`, same API with `[ObservableProperty]` `State`.
- **INavigationService** — Page navigation contract typed on `INotifyPropertyChanged`. Works with both `ViewContext` and `ViewModelBase` families.
- **NavigationService** — Concrete implementation with `Stack<>` back-stack and `INavigationAware` lifecycle notification.
- **PageFactory** — DI-aware page resolution via `IServiceProvider`.
- **ViewLocator** — `IDataTemplate` that maps `*ViewModel` → `*View` and `*ViewContext` → `*View` by naming convention.
- **TiferetPalette** — Immutable palette record with `Light()`, `Dark()`, and `Custom(...)` factory methods.
- **TiferetTheme** — AXAML-loaded Avalonia `Styles` entry point with `ResolvePalette()` for options-driven palette selection.
- **AvaloniaBlueprint** — Static bootstrap that wires core Tiferet services + Avalonia-specific services (theme, palette, options).

### Design Constraints

- Navigation contracts are typed on `INotifyPropertyChanged`, not on a specific base class — both `ViewContext` and `ViewModelBase` families work interchangeably.
- `DomainViewContext` and `DomainViewModel` constrain on `DomainObject` (not `Aggregate`) to maintain separation between the view layer and the mapper layer.
- `TiferetPalette` is an immutable record. `Custom(...)` creates a new instance with selective overrides from a base palette.

## Structured Code Style

All code follows the Tiferet structured code style with artifact comments:

### Comment Levels

- `# ***` — Top-level: `imports`, `contexts`, `interfaces`, `navigation`, `records`, `constants`, `styles`, `extensions`
- `# **` — Mid-level: `context: <name>`, `interface: <name>`, `navigation: <name>`, `record: <name>`, `factory: <name>`, `property: <name>`, `test: <name>`
- `# *` — Low-level: `attribute: <name>`, `init`, `method: <name>`, `method: <name> (static)`, `event: <name>`

### Spacing Rules

- One empty line between `# ***` and first `# **`.
- One empty line between each `# *` section.
- One empty line after docstrings and between code snippets within methods.

### Docstrings

Use XML doc comments (`/// <summary>`) with `<param>`, `<typeparam>`, `<returns>` for all public members.

## Dependencies

### NuGet Packages (Tiferet.Avalonia)

- `Tiferet` 1.0.0-beta.7 (local nupkg)
- `CommunityToolkit.Mvvm` 8.4.0
- `Avalonia` 11.2.7
- `Avalonia.Desktop` 11.2.7
- `Avalonia.Themes.Fluent` 11.2.7

### NuGet Packages (Test Project)

- `xunit` 2.9.2
- `Moq` 4.20.72
- `Microsoft.NET.Test.Sdk` 17.12.0

### Local NuGet Feed

The `local-packages/` directory contains local `.nupkg` files. The `nuget.config` at the repo root configures this as a package source alongside nuget.org.

## Testing

- **Framework:** xUnit (with `Moq` for mocking).
- **Test location:** `tests/Tiferet.Avalonia.Tests/` — organized by component (`Navigation/`, `Assets/`, `Contexts/`).
- **Run tests:** `dotnet test` from repo root.
- **Test structure:** Uses Tiferet artifact comments (`# *** fixtures`, `# ** fixture:`, `# *** tests`, `# ** test:`).
- **Naming:** Test methods use `MethodName_Condition_ExpectedResult` pattern.

## Configuration

- `nuget.config` — Package sources (nuget.org + local-packages).
- `Directory.Build.props` — Shared version (`1.0.0-beta.2`) and NuGet metadata.
- `Tiferet.Avalonia.sln` — Solution containing `Tiferet.Avalonia`, `Tiferet.Avalonia.Tests`, and `Tiferet.Avalonia.Examples.Calculator`.

## Key Files for Orientation

- `Tiferet.Avalonia/Contexts/ViewContext.cs` — Manual INPC base class
- `Tiferet.Avalonia/Contexts/ViewModelBase.cs` — CommunityToolkit MVVM base class
- `Tiferet.Avalonia/Contexts/DomainViewModel.cs` — Domain model binding with toolkit
- `Tiferet.Avalonia/Contexts/DomainViewContext.cs` — Domain model binding with manual INPC
- `Tiferet.Avalonia/Navigation/NavigationService.cs` — Concrete navigation with back-stack
- `Tiferet.Avalonia/Navigation/ViewLocator.cs` — Convention-based view resolution
- `Tiferet.Avalonia/Assets/Design/Palette.cs` — Palette definitions (Light, Dark, Custom)
- `Tiferet.Avalonia/Assets/Styles/TiferetTheme.cs` — Theme entry point + ResolvePalette
- `Tiferet.Avalonia/Blueprints/AvaloniaBlueprint.cs` — Bootstrap wiring
- `Tiferet.Avalonia/Blueprints/TiferetAvaloniaOptions.cs` — Configuration POCO
- `Tiferet.Avalonia/DependencyInjection/ServiceCollectionExtensions.cs` — DI registration

## Branch Conventions

- Feature branches: `<issue-number>-<lowercase-hyphenated-title>`
- PRs target the prototype branch (`v1.x-proto`).
- All commits include `Co-Authored-By: Oz <oz-agent@warp.dev>` when collaborating with AI.
