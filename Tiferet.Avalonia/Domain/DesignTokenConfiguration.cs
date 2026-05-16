using Tiferet.Domain;

namespace Tiferet.Avalonia.Domain;

// *** models

// ** model: design_token_configuration
/// <summary>
/// Domain record for design token overrides, enabling YAML-driven customization
/// of spacing, corner radii, and font family.
/// </summary>
public sealed record DesignTokenConfiguration(
    string? FontFamily = null,
    double? SpacingSM = null,
    double? SpacingMD = null,
    double? SpacingLG = null,
    double? CornerRadiusSM = null,
    double? CornerRadiusMD = null,
    double? CornerRadiusLG = null) : DomainObject;
