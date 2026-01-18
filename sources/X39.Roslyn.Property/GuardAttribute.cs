using System;

namespace X39.Roslyn.Property;

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
    /// Gets or sets the arguments that will be passed to the guard method during validation.
    /// </summary>
    /// <remarks>
    /// These arguments provide additional data that may be required by the guard method to validate changes to the property.
    /// The arguments must match the parameters expected by the guard method beyond the required `oldValue` and `newValue`;
    /// Sample method signature: `bool GuardMethodName(T oldValue, T newValue, string arg1, int arg2, ...)`
    /// </remarks>
    public object?[]? Arguments { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the old value of the property
    /// should be passed to the guard method during validation.
    /// </summary>
    /// <remarks>
    /// If set to <see langword="true"/>, the guard method is expected to accept the old value as a parameter.
    /// If set to <see langword="false"/>, the guard method will not be provided with the old value.
    /// Sample method signatures:
    /// <list type="bullet">
    /// <item>HasOldValue = false: `bool GuardMethodName(T newValue)`</item>
    /// <item>HasOldValue = true: `bool GuardMethodName(T oldValue, T newValue)`</item>
    /// </list>
    /// </remarks>
    public bool HasOldValue { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the guard method will be provided with the new value of the property during validation.
    /// </summary>
    /// <remarks>
    /// If set to <see langword="true"/>, the guard method is expected to accept the new value as a parameter.
    /// If set to <see langword="false"/>, the guard method will not be provided with the new value.
    /// Sample method signatures:
    /// <list type="bullet">
    /// <item>HasNewValue = false: `bool GuardMethodName(T oldValue)`</item>
    /// <item>HasNewValue = true: `bool GuardMethodName(T oldValue, T newValue)`</item>
    /// <item>HasOldValue = false: `bool GuardMethodName(T newValue)`</item>
    /// <item>HasOldValue = false, HasNewValue = false: `bool GuardMethodName()`</item>
    /// </list>
    /// </remarks>
    public bool HasNewValue { get; set; } = true;

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