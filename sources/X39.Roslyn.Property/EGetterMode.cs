namespace X39.Roslyn.Property;

/// <summary>
/// Enum containing the possible getter modes for the <see cref="GetterAttribute"/>.
/// </summary>
public enum EGetterMode
{
    /// <summary>
    /// The default behavior, leaving the choice to the generator.
    /// </summary>
    /// <remarks>
    /// As this is the getter mode,
    /// the default behavior is always to generate the getter.
    /// </remarks>
    Default,

    /// <summary>
    /// The getter will not be generated.
    /// </summary>
    None,
}