namespace X39.Roslyn.Property;

/// <summary>
/// Represents the mode for generating setter behavior in a property.
/// </summary>
public enum ESetterMode
{
    /// <summary>
    /// The default behavior, leaving the choice to the generator.
    /// </summary>
    /// <remarks>
    /// The default behavior is to either generate a setter
    /// using set if the field is not readonly
    /// or generate it using init if the field is readonly.
    /// </remarks>
    Default,

    /// <summary>
    /// The setter will be forced to generate using the set keyword.
    /// </summary>
    /// <remarks>
    /// This behavior will break readonly fields.
    /// use with caution.
    /// </remarks>
    Set,

    /// <summary>
    /// The setter will be forced to generate using the init keyword.
    /// </summary>
    Init,

    /// <summary>
    /// The setter will not be generated.
    /// </summary>
    None,
}