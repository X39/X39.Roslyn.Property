using System;

namespace X39.Roslyn.Property;

/// <summary>
/// Makes the field or class, annotated with the 'NotifyPropertyChanged' attribute, create
/// properties which raise the PropertyChanged event when changed.
/// For a decorated class, all fields will be considered for property generation and the
/// PropertyChanged event will be implemented too.
/// </summary>
/// <remarks>
/// The name of the property generated will be the Capitalized name of the field, minus any leading underscores.
/// If the field is annotated with the 'PropertyName' attribute,
/// the name of the property will be the value of the 'PropertyName' attribute.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class NotifyPropertyChangedAttribute : Attribute
{
    /// <summary>
    /// Indicates whether the PropertyChanged event should be generated for the decorated
    /// class or fields when the associated value changes.
    /// </summary>
    public bool GenerateEvent { get; }
    
    /// <summary>
    /// Defines the name of the method to be called when the PropertyChanged event is raised.
    /// </summary>
    /// <remarks>
    /// Method must have the following signature:
    /// <code>
    /// void MethodName(object, string)
    /// </code>
    /// </remarks>
    public string? CallMethod { get; }

    /// <summary>
    /// Makes the field or class, annotated with the 'NotifyPropertyChanged' attribute, create
    /// properties which raise the PropertyChanged event when changed.
    /// For a decorated class, all fields will be considered for property generation and the
    /// PropertyChanged event will be implemented too.
    /// </summary>
    /// <remarks>
    /// The name of the property generated will be the Capitalized name of the field, minus any leading underscores.
    /// If the field is annotated with the 'PropertyName' attribute,
    /// the name of the property will be the value of the 'PropertyName' attribute.
    /// </remarks>
    /// <param name="generateEvent">
    ///     If true, the PropertyChanged event will be generated (as in: Added to the generated class).
    ///     If false, the PropertyChanged event will not be generated and must be supplied by the user.
    /// </param>
    /// <param name="callMethod">
    /// The name of the method to be called when the PropertyChanged event is raised.
    /// </param>
    public NotifyPropertyChangedAttribute(bool generateEvent = true, string? callMethod = null)
    {
        GenerateEvent = generateEvent;
        CallMethod    = callMethod;
    }
}

#pragma warning disable CS1574, CS1584, CS1581, CS1580
#pragma warning restore CS1574, CS1584, CS1581, CS1580