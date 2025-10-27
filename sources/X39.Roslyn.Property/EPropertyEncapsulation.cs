namespace X39.Roslyn.Property;

/// <summary>
/// Enum containing the possible property encapsulation strategies for the <see cref="PropertyEncapsulationAttribute"/>.
/// </summary>
public enum EPropertyEncapsulation
{
    /// <summary>
    /// The property will be public and accessible from anywhere.
    /// </summary>
    Public,

    /// <summary>
    /// The property will be protected and accessible from the containing class and its derived classes.
    /// </summary>
    Protected,

    /// <summary>
    /// The property will be private and accessible only from the containing class.
    /// </summary>
    Private,

    /// <summary>
    /// The property will be public and accessible only from the containing assembly.
    /// </summary>
    Internal,

    /// <summary>
    /// The property will be protected public and accessible from the containing class or the derived
    /// classes living in the same assembly.
    /// </summary>
    ProtectedInternal,
}