using System;

namespace X39.SourceGenerators.Property;

/// <summary>
/// Allows to guard a property from being changed, failing the validation if the guard method does not return true.
/// </summary>
/// <remarks>
/// The guard method will be looked up in the class containing the property unless the
/// className parameter is set.
/// The method must be accessible from the containing class, must return a <see cref="bool"/>
/// and must accept two parameters.
/// Sample method signature: `bool GuardMethodName(T oldValue, T newValue)`.
/// </remarks>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
public class GuardAttribute : Attribute
{
    /// <summary>
    /// Gets the name of the guard method that will be invoked to validate changes to the associated property.
    /// </summary>
    public string MethodName { get; }

    /// <summary>
    /// Specifies the name of the class containing the guard method to be used for validating property value changes.
    /// </summary>
    /// <remarks>
    /// If this property is null, the guard method will be assumed to reside in the same class as the property.
    /// The class must contain a method with the appropriate signature as described in the documentation for the <see cref="GuardAttribute"/>.
    /// </remarks>
    public string? ClassName { get; }

    /// <summary>
    /// Allows to guard a property from being changed, failing the validation if the guard method does not return true.
    /// </summary>
    /// <remarks>
    /// The guard method will be looked up in the class containing the property unless the
    /// <paramref name="className"/> parameter is set.
    /// The method must be accessible from the containing class, must return a <see cref="bool"/>
    /// and must accept two parameters.
    /// Sample method signature: `bool GuardMethodName(T oldValue, T newValue)`.
    /// </remarks>
    public GuardAttribute(string methodName, string? className = null)
    {
        MethodName = methodName;
        ClassName  = className;
    }
}