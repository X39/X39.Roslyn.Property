using System;

namespace X39.Roslyn.Property;

/// <summary>
/// Makes the field annotated with the 'PropertyName' attribute, create a property with the specified name.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class PropertyNameAttribute : Attribute
{
    /// <summary>
    /// Gets the name of the property to be created for a field annotated with the 'PropertyName' attribute.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Makes the field annotated with the 'PropertyName' attribute, create a property with the specified name.
    /// </summary>
    public PropertyNameAttribute(string name)
    {
        Name = name;
    }
}