using System;

namespace X39.Roslyn.Property;

/// <summary>
/// Specifies the default value for a property as a C# expression string.
/// </summary>
/// <remarks>
/// The <see cref="DefaultValueAsStringAttribute"/> can be applied to properties or fields.
/// It is used to define a default value using a C# expression string, which is useful
/// for values that cannot be represented as compile-time constants (e.g., <c>new object()</c>).
/// </remarks>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class DefaultValueAsStringAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultValueAsStringAttribute"/> class.
    /// </summary>
    /// <param name="value">The C# expression string representing the default value.</param>
    public DefaultValueAsStringAttribute(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the C# expression string representing the default value.
    /// </summary>
    public string Value { get; }
}
