using System;

namespace X39.Roslyn.Property;

/// <summary>
/// Allows to change the generator behavior for the getter of a property.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class GetterAttribute : Attribute
{
    /// <summary>
    /// The getter mode to use.
    /// </summary>
    public EGetterMode Mode { get; }

    /// <summary>
    /// Allows to change the generator behavior for the getter of a property.
    /// </summary>
    /// <param name="mode">The getter mode to use.</param>
    public GetterAttribute(EGetterMode mode = EGetterMode.Default)
    {
        Mode = mode;
    }
}