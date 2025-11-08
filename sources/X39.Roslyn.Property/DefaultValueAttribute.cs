using System;

namespace X39.Roslyn.Property;

/// <summary>
/// Specifies the default value for a property.
/// </summary>
/// <typeparam name="T">The type of the default value.</typeparam>
/// <remarks>
/// The <see cref="DefaultValueAttribute{T}"/> can be applied to properties.
/// It is used to define a default value, which can be utilized in scenarios where
/// an assigned value is required, but the property has not been explicitly set.
/// </remarks>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class DefaultValueAttribute<T> : Attribute
{
    /// <summary>
    /// Represents an attribute that specifies a default value for a property.
    /// </summary>
    /// <typeparam name="T">The type of the default value.</typeparam>
    /// <remarks>
    /// This attribute can be used to define a default value for properties,
    /// providing a way to specify a value to be used when the property has not been explicitly set.
    /// </remarks>
    public DefaultValueAttribute(T value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the default value specified by the attribute.
    /// </summary>
    /// <remarks>
    /// The value represents the default value assigned to a property when this attribute is applied.
    /// It can provide a fallback value for scenarios where the property has not been explicitly set.
    /// </remarks>
    public T Value { get; }
}