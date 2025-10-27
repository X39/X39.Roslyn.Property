using System;

namespace X39.Roslyn.Property;

/// <summary>
/// Allows changing the equality check for a property or disabling it entirely.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class EqualityCheckAttribute : Attribute
{
    /// <summary>
    /// Specifies the equality check mode to be applied by the <see cref="EqualityCheckAttribute"/>.
    /// Determines the type or nature of the equality comparison.
    /// </summary>
    public EEqualityCheckMode Mode { get; }

    /// <summary>
    /// Specifies the tolerance level for equality checks of floating-point properties.
    /// This value is used when comparing float values in order to determine if they are
    /// considered equal within the specified precision.
    /// </summary>
    public float FloatEpsilon { get; }

    /// <summary>
    /// Defines the precision threshold used in equality checks for double-precision floating-point values.
    /// </summary>
    public double DoubleEpsilon { get; }

    /// <summary>
    /// Gets or sets a user-defined string key used to modify the behavior of the equality check
    /// when the mode is set to custom.
    /// </summary>
    public string? Custom { get; }

    /// <summary>
    /// Allows changing the equality check for a property or disabling it entirely.
    /// </summary>
    public EqualityCheckAttribute(
        EEqualityCheckMode mode = EEqualityCheckMode.Default,
        float floatEpsilon = Single.Epsilon,
        double doubleEpsilon = Double.Epsilon,
        string? custom = null
    )
    {
        Mode               = mode;
        this.FloatEpsilon  = floatEpsilon;
        this.DoubleEpsilon = doubleEpsilon;
        this.Custom        = custom;
    }
}