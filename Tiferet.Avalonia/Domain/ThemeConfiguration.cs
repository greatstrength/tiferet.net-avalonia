using Tiferet.Domain;

namespace Tiferet.Avalonia.Domain;

// *** models

// ** model: theme_configuration
/// <summary>
/// Domain record for theme settings, enabling YAML-driven theme customization.
/// </summary>
public sealed record ThemeConfiguration(
    string Id,
    string Name,
    string Variant,
    string? PrimaryColor = null,
    string? SecondaryColor = null,
    string? FontFamily = null) : DomainObject;
