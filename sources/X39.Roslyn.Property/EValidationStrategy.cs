namespace X39.Roslyn.Property;

/// <summary>
/// Enum containing the possible validation strategies for the <see cref="ValidationStrategyAttribute"/>.
/// </summary>
public enum EValidationStrategy
{
    /// <summary>
    /// Throw an exception when the validation fails.
    /// </summary>
    /// <remarks>
    /// This is the default behavior.
    /// </remarks>
    Exception,

    /// <summary>
    /// Rollback the changes when the validation fails.
    /// </summary>
    /// <remarks>
    /// Unless another property (eg. NotifyPropertyChanged) is also added,
    /// this mode behaves like the <see cref="Ignore"/> mode.
    /// If such a property is present, the corresponding side effects will be taken into account
    /// (eg. PropertyChanged event will be raised to notify the UI about the change in the value, albeit the
    /// value not having been changed in the underlying data store).
    /// </remarks>
    Rollback,

    /// <summary>
    /// Ignore the changes when the validation fails.
    /// </summary>
    /// <remarks>
    /// If changes are ignored, the code will exit immediately without any further action (without side effects).
    /// </remarks>
    Ignore,
}