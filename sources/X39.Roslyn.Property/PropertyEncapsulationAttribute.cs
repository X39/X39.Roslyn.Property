using System;

namespace X39.Roslyn.Property;

/// <summary>
/// Allows to configure either the default property encapsulation (when applied to a class)
/// or the property encapsulation for a specific field.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class PropertyEncapsulationAttribute : Attribute
{
    /// <summary>
    /// Represents the level of encapsulation applied to a property, defining its accessibility,
    /// such as public, protected, private, internal, or protected internal.
    /// </summary>
    public EPropertyEncapsulation Encapsulation { get; }

    /// <summary>
    /// Allows to configure either the default property encapsulation (when applied to a class)
    /// or the property encapsulation for a specific field.
    /// </summary>
    public PropertyEncapsulationAttribute(EPropertyEncapsulation encapsulation = EPropertyEncapsulation.Public)
    {
        Encapsulation = encapsulation;
    }
}