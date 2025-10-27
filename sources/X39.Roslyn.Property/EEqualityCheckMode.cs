namespace X39.Roslyn.Property;

/// <summary>
/// Enum containing the possible equality check modes for the <see cref="EqualityCheckAttribute"/>.
/// </summary>
public enum EEqualityCheckMode
{
    /// <summary>
    /// The default behavior, using `Equals` for reference types,
    /// `==` for value types and epsilon comparison for floating point types.
    /// </summary>
    /// <seealso cref="float.Epsilon"/>
    /// <seealso cref="double.Epsilon"/>
    Default,

    /// <summary>
    /// The property will be checked for equality using the provided custom comparison.
    /// Here, `value` will be the new value and the field name will be the old value.
    /// All class members may be accessed from within the custom comparison.
    /// </summary>
    /// <seealso cref="GuardAttribute"/>
    Custom,

    /// <summary>
    /// The property will not be checked for equality.
    /// </summary>
    /// <remarks>
    /// Neither the <see cref="ValidationStrategyAttribute"/> nor the <see cref="GuardAttribute"/> depend on this setting.
    /// </remarks>
    None,
}