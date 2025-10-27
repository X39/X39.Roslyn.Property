using System;

namespace X39.SourceGenerators.Property;

/// <summary>
/// By default, the generator will take over the properties of the annotated fields.
/// This attribute allows to disable the takeover for a specific field or the entire class.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class DisableAttributeTakeoverAttribute : Attribute { }