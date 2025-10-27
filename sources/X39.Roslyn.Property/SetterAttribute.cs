using System;

namespace X39.Roslyn.Property;

/// <summary>
/// Specifies that the field annotated with the 'Setter' attribute should generate a setter method.
/// This also may be used to only allow the setter to be initialized.
/// </summary>
/// <remarks>
/// By default, the setter method will be generated according to the specified 'ESetterMode' value.
/// </remarks>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class SetterAttribute : Attribute
{
    /// <summary>
    /// The getter mode to use.
    /// </summary>
    public ESetterMode Mode { get; }

    /// <summary>
    /// Specifies the behavior of the setter method generated for a field annotated with the 'Setter' attribute.
    /// </summary>
    public SetterAttribute(ESetterMode mode = ESetterMode.Default)
    {
        Mode = mode;
    }
}