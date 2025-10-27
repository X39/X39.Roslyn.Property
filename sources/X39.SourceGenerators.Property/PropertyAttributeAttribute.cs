using System;

namespace X39.SourceGenerators.Property;

/// <summary>
/// Allows to add attributes to the generated properties which normally would not be allowed on eg. fields or classes.
/// </summary>
/// <remarks>
/// This attribute will, unless explicitly requested, override the defaults set by a class annotated with this,
/// when used on a field.
/// The default inheritance behavior cannot be changed when used on a class,
/// but only one field has to change the inheritance behavior to inherit.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class PropertyAttributeAttribute : Attribute
{
    /// <summary>
    /// Represents the full attribute definition as it would normally be written in code. This property defines
    /// the structure and specification of the attribute to be applied to generated properties, fields, or classes.
    /// </summary>
    public string AttributeDefinition { get; }

    /// <summary>
    /// Determines whether the attribute defined at the field level inherits from the class-level attribute.
    /// </summary>
    /// <remarks>
    /// When set to <see langword="true"/>, the field-level attribute appends to the class-level attributes.
    /// When set to <see langword="false"/>, the field-level attribute overrides the class-level attributes.
    /// This property does not affect inheritance behavior when used on a class; it is applicable only at the field level.
    /// </remarks>
    public bool Inherit { get; }

    /// <summary>
    /// Allows to add attributes to the generated properties which normally would not be allowed on eg. fields or classes.
    /// </summary>
    /// <param name="attributeDefinition">The full attribute definition as it would normally be written in code.</param>
    /// <param name="inherit">
    ///     When <see langword="true"/>, the attribute will be appended to the class defined attributes.
    ///     When <see langword="false"/>, the attribute will override the class defined attributes.
    /// The default inheritance behavior cannot be changed when used on a class,
    /// but only one field has to change the inheritance behavior to inherit.
    /// </param>
    /// <example>
    /// <code>
    /// [PropertyAttribute("Range(0, 100)")]
    /// [PropertyAttribute("[Range(0, 100)]")]
    /// [PropertyAttribute("[Range(0, 100), MaxLength(10)]")]
    /// </code>
    /// </example>
    public PropertyAttributeAttribute(string attributeDefinition, bool inherit = false)
    {
        AttributeDefinition = attributeDefinition;
        Inherit             = inherit;
    }
}