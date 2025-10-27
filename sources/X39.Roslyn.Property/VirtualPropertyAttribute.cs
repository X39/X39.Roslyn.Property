using System;

namespace X39.Roslyn.Property;

/// <summary>
/// When applied to a field, it will make the property generated virtual, allowing to override it in derived classes.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class VirtualPropertyAttribute : Attribute { }